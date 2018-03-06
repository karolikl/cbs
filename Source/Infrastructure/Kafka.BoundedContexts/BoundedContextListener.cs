/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017 International Federation of Red Cross. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using doLittle.Applications;
using doLittle.Events;
using doLittle.Execution;
using doLittle.Logging;
using doLittle.Runtime.Events;
using doLittle.Runtime.Events.Coordination;
using doLittle.Runtime.Events.Publishing.InProcess;
using doLittle.Runtime.Events.Storage;
using doLittle.Runtime.Transactions;
using doLittle.Types;
using Newtonsoft.Json;

namespace Infrastructure.Kafka.BoundedContexts
{
    [Singleton]
    public class BoundedContextListener : IBoundedContextListener
    {
        public static TransactionCorrelationId CorrelationId {  get; set; }
        readonly IEventConverter _eventConverter;
        readonly IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
        readonly ILogger _logger;
        readonly IApplicationArtifactIdentifierStringConverter _applicationResourceIdentifierConverter;
        readonly IImplementationsOf<IEvent> _eventTypes;
        readonly IEventSequenceNumbers _eventSequenceNumbers;
        readonly IEventStore _eventStore;
        readonly IEventEnvelopes _eventEnvelopes;
        readonly IEventSourceVersions _eventSourceVersions;
        readonly ICommittedEventStreamBridge _committedEventStreamBridge;
        readonly JsonSerializer _serializer;
        readonly IConsumer _consumer;
        readonly BoundedContextListenerConfiguration _configuration;

        public BoundedContextListener(
            BoundedContextListenerConfiguration configuration,
            IEventConverter eventConverter,
            IUncommittedEventStreamCoordinator uncommittedEventStreamCoordinator,
            ILogger logger,
            IApplicationArtifactIdentifierStringConverter applicationResourceIdentifierConverter,
            IImplementationsOf<IEvent> eventTypes,
            IEventStore eventStore,
            IEventEnvelopes eventEnvelopes,
            IEventSequenceNumbers eventSequenceNumbers,
            IEventSourceVersions eventSourceVersions,
            ICommittedEventStreamBridge committedEventStreamBridge,
            IConsumer consumer)
        {
            _eventConverter = eventConverter;
            _uncommittedEventStreamCoordinator = uncommittedEventStreamCoordinator;
            _logger = logger;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _eventTypes = eventTypes;
            _eventSequenceNumbers = eventSequenceNumbers;
            _eventStore = eventStore;
            _eventEnvelopes = eventEnvelopes;
            _eventSourceVersions = eventSourceVersions;
            _committedEventStreamBridge = committedEventStreamBridge;
            _consumer = consumer;

            _serializer = new JsonSerializer();
            _configuration = configuration;
        }


        void Received(Topic topic, string eventAsJson)
        {
            try
            {
                _logger.Trace($"Message received 'eventAsJson'");
                dynamic raw = JsonConvert.DeserializeObject(eventAsJson);

                foreach( var rawContentAndEnvelope in raw ) 
                {
                    var eventSourceId = (EventSourceId)Guid.Parse(rawContentAndEnvelope.Content.EventSourceId.ToString());
                    var eventIdentifier = _applicationResourceIdentifierConverter.FromString(rawContentAndEnvelope.Envelope.Event.ToString());
                    var version = EventSourceVersion.FromCombined((double)rawContentAndEnvelope.Envelope.Version);
                    var correlationId = (TransactionCorrelationId)Guid.Parse(rawContentAndEnvelope.Envelope.CorrelationId.ToString());
                    var eventSource = new ExternalSource(eventSourceId);
                    CorrelationId = correlationId;
                    
                    _logger.Trace($"Received event of with resource name '{eventIdentifier.Resource.Name}' from '{eventSourceId}' with version '{version}' in correlation '{correlationId}'");
                    var eventType = _eventTypes.SingleOrDefault(et => et.Name == eventIdentifier.Resource.Name);
                    if( eventType != null )
                    {
                        _logger.Trace("Matching Event Type : " + eventType.AssemblyQualifiedName);
                        var @event = GetEventFrom(rawContentAndEnvelope, eventSourceId, eventType);
                        
                        var uncommittedEventStream = new UncommittedEventStream(eventSource);
                        uncommittedEventStream.Append(@event, version);


                        _logger.Information($"Committing uncommitted event stream with correlationId '{correlationId}'");
                        var envelopes = _eventEnvelopes.CreateFrom(eventSource, uncommittedEventStream.EventsAndVersion);
                        
                        var envelopesAsArray = envelopes.ToArray();
                        var eventsAsArray = uncommittedEventStream.ToArray();

                        _logger.Trace("Create an array of events and envelopes");
                        var eventsAndEnvelopes = new List<EventAndEnvelope>();
                        for (var eventIndex = 0; eventIndex < eventsAsArray.Length; eventIndex++)
                        {
                            var envelope = envelopesAsArray[eventIndex];
                            var currentEvent = eventsAsArray[eventIndex];
                            eventsAndEnvelopes.Add(new EventAndEnvelope(
                                envelope
                                    .WithTransactionCorrelationId(correlationId)
                                    .WithSequenceNumber(_eventSequenceNumbers.Next())
                                    .WithSequenceNumberForEventType(_eventSequenceNumbers.NextForType(envelope.Event)),
                                currentEvent
                            ));
                        }

                        _logger.Trace("Committing events to event store");
                        _eventStore.Commit(eventsAndEnvelopes);

                        _logger.Trace($"Set event source versions for the event source '{envelopesAsArray[0].EventSource}' with id '{envelopesAsArray[0].EventSourceId}'");
                        _eventSourceVersions.SetFor(envelopesAsArray[0].EventSource, envelopesAsArray[0].EventSourceId, envelopesAsArray[envelopesAsArray.Length - 1].Version);

                        _logger.Trace("Create a committed event stream");
                        var committedEventStream = new CommittedEventStream(uncommittedEventStream.EventSourceId, eventsAndEnvelopes);
                        _committedEventStreamBridge.Send(committedEventStream);

                        CorrelationId = Guid.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during receiving");

            }
        }

        IEvent GetEventFrom(dynamic rawContentAndEnvelope, EventSourceId eventSourceId, Type eventType)
        {
            var @event = Activator.CreateInstance(eventType, eventSourceId) as IEvent;
            using (var textReader = new StringReader(rawContentAndEnvelope.Content.ToString()))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    _serializer.Populate(reader, @event);
                }
            }

            return @event;
        }

        public void Start()
        {
            _consumer.SubscribeTo($"BoundedContextListenerFor_{_configuration.Topic}",_configuration.Topic, Received);
        }

        public static void Start(IServiceProvider serviceProvider)
        {
            var listener = serviceProvider.GetService(typeof(IBoundedContextListener)) as IBoundedContextListener;
            listener.Start();
        }
    }
}
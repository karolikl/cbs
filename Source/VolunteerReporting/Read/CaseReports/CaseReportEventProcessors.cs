/*---------------------------------------------------------------------------------------------
 *  Copyright (c) The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Concepts.DataCollector;
using Dolittle.Events.Processing;
using Dolittle.ReadModels;
using Events.CaseReports;

namespace Read.CaseReports
{
    public class CaseReportEventProcessor : ICanProcessEvents
    {
        readonly IReadModelRepositoryFor<CaseReport> _caseReports;
        readonly IReadModelRepositoryFor<CaseReportFromUnknownDataCollector> _caseReportsFromUnknownDataCollectors;

        public CaseReportEventProcessor(
            IReadModelRepositoryFor<CaseReport> caseReports,
            IReadModelRepositoryFor<CaseReportFromUnknownDataCollector> caseReportsFromUnknownDataCollectors)
        {
            _caseReports = caseReports;
            _caseReportsFromUnknownDataCollectors = caseReportsFromUnknownDataCollectors;
        }
        [EventProcessor("7f3b6037-6b2f-448b-8f14-0735330a50e0")]
        public void Process(CaseReportReceived @event)
        {
            var caseReport = new CaseReport(@event.CaseReportId)
            {
                DataCollectorId = @event.DataCollectorId,
                HealthRiskId = @event.HealthRiskId,
                NumberOfFemalesUnder5 = @event.NumberOfFemalesUnder5,
                NumberOfFemalesAged5AndOlder = @event.NumberOfFemalesAged5AndOlder,
                NumberOfMalesUnder5 = @event.NumberOfMalesUnder5,
                NumberOfMalesAged5AndOlder = @event.NumberOfMalesAged5AndOlder,
                Location = new Location(@event.Latitude, @event.Longitude),
                Timestamp = @event.Timestamp,
                Message = @event.Message
            };

            _caseReports.Insert(caseReport);
        }
        [EventProcessor("980f8db1-2e3a-4609-b7e6-29cee5190ea8")]
        public void Process(CaseReportFromUnknownDataCollectorReceived @event)
        {
            var caseReport = new CaseReportFromUnknownDataCollector(@event.CaseReportId)
            {
                Origin = @event.Origin,
                HealthRiskId = @event.HealthRiskId,
                NumberOfFemalesUnder5 = @event.NumberOfFemalesUnder5,
                NumberOfFemalesAged5AndOlder = @event.NumberOfFemalesAged5AndOlder,
                NumberOfMalesUnder5 = @event.NumberOfMalesUnder5,
                NumberOfMalesAged5AndOlder = @event.NumberOfMalesAged5AndOlder,
                Timestamp = @event.Timestamp,
                Message = @event.Message
            };
            _caseReportsFromUnknownDataCollectors.Insert(caseReport);
        }
        [EventProcessor("cc5e94eb-7944-419d-9637-1a6807e8991c")]
        public void Process(CaseReportIdentified @event)
        {
            var caseReport = _caseReportsFromUnknownDataCollectors.GetById(@event.CaseReportId);
            _caseReportsFromUnknownDataCollectors.Delete(caseReport);            
        }
    }
}

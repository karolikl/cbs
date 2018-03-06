/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017 International Federation of Red Cross. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Infrastructure.Kafka.BoundedContexts
{
    public class CommittedEventStreamSenderConfiguration
    {
        public CommittedEventStreamSenderConfiguration(IEnumerable<Topic> topics)
        {
            Topics = topics;
        }

        public IEnumerable<Topic> Topics { get; }
    }
}
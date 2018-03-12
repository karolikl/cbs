/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017 International Federation of Red Cross. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Infrastructure.Kafka
{
    public interface IConfiguration
    {
        KafkaConnectionString ConnectionString { get; }
        
        Dictionary<string, object> GetForPublisher();
        Dictionary<string, object> GetFor(string consumer);
    }
}
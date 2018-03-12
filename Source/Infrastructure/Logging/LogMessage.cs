/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017-2018 The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Infrastructure.Logging
{
    public class LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Source { get; set; }
        public Guid CorrelationId { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public int Line { get; set; }
        public object Content { get; set; }
    }
}
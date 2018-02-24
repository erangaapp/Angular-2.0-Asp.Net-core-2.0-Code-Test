using System;
using System.Collections.Generic;
using System.Text;

using CT.Models.Base;

namespace CT.Models
{
    public class EventLog : BaseEntity
    {
        public int EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Event { get; set; }

    }
}

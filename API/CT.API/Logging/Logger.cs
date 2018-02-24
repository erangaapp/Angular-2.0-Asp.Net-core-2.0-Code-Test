using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ABS = CT.Service.Abstract;

namespace CT.API.Logging
{
    public class Logger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private SqlHelper _helper;
        private int MessageMaxLength = 4000;

        public Logger(string categoryName, Func<string, LogLevel, bool> filter, string connectionString)
        {
            _categoryName = categoryName;
            _filter = filter;
            _helper = new SqlHelper(connectionString);
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            
            if (exception != null)
            {
                message += "\n" + exception.ToString();

                message = message.Length > MessageMaxLength ? message.Substring(0, MessageMaxLength) : message;
                EventLog eventLog = new EventLog
                {
                    Message = message,
                    EventId =  eventId.Id,
                    LogLevel = logLevel.ToString(),
                    CreatedTime = DateTime.UtcNow,
                    Event = string.IsNullOrEmpty(eventId.Name) ? "Unhandled Exception" : eventId.Name
                };
                await _helper.InsertLog(eventLog);

            }

        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }

    public class EventLog
    {
        public string Message { get; set; }
        public int EventId { get; set; }
        public string LogLevel { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Event { get; set; }
    }
}

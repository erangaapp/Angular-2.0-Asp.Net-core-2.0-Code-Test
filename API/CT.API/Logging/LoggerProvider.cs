using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.API.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private string _connectionString;


        public LoggerProvider(Func<string, LogLevel, bool> filter, string connectionString)
        {
            _filter = filter;
            _connectionString = connectionString;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName, _filter, _connectionString);
        }

        public void Dispose()
        {

        }
    }
}

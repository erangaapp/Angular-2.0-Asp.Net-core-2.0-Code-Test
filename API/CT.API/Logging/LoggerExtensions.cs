using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.API.Logging
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory, string connectionString, Func<string, LogLevel, bool> filter = null)
        {
            factory.AddProvider(new LoggerProvider(filter, connectionString));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, string connectionString, LogLevel minLevel)
        {
            return AddContext(
                factory,
                connectionString,
                (_, logLevel) => logLevel >= minLevel);
        }
    }
}

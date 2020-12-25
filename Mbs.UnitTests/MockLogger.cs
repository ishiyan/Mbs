using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Mbs.UnitTests
{
    internal class MockLogger : ILogger
    {
        public List<Info> LogInfoList { get; } = new List<Info>();

        public void Errors(out int errorCount, out string lastErrorText)
        {
            var errors = LogInfoList.FindAll(info => info.LogLevel == LogLevel.Error || info.LogLevel == LogLevel.Critical);
            errorCount = errors.Count;
            lastErrorText = errorCount > 0 ? errors.Last().Message : null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LogInfoList.Add(new Info
            {
                LogLevel = logLevel, Exception = exception, Message = formatter(state, exception),
            });
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public class Info
        {
            public LogLevel LogLevel { get; set; }

            public Exception Exception { get; set; }

            public string Message { get; set; }
        }
    }
}
using System;
using System.Globalization;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Mbs
{
    /// <summary>
    /// A simple wrapper over the <see cref="ILogger"/>.
    /// </summary>
    public static class Log
    {
        private static ILogger logger;
        private static ILoggerFactory loggerFactory;

        /// <summary>
        /// Creates an <see cref="ILogger&lt;T&gt;"/> for a given type T.
        /// </summary>
        /// <typeparam name="T">A type to create a logger for.</typeparam>
        /// <returns>An instance of the logger.</returns>
        public static ILogger<T> CreateLogger<T>()
        {
            return loggerFactory.CreateLogger<T>();
        }

        /// <summary>
        /// Sets the <see cref="ILoggerFactory"/>.
        /// </summary>
        /// <param name="factory">The <see cref="ILoggerFactory"/> to set.</param>
        public static void SetLoggerFactory(ILoggerFactory factory)
        {
            loggerFactory = factory;
        }

        /// <summary>
        /// Sets the <see cref="ILogger"/>.
        /// </summary>
        /// <param name="log">The <see cref="ILogger"/> to set.</param>
        public static void SetLogger(ILogger log)
        {
            logger = log;
        }

        private static readonly Action<ILogger, string, int, string, Exception> DailyOhlcvBarsDownloadedAction =
            LoggerMessage.Define<string, int, string>(
                LogLevel.Information,
                new EventId(1, nameof(DailyOhlcvBarsDownloaded)),
                "{Entity} downloaded {Count} ohlcv bars from '{Uri}'");

        /// <summary>
        /// Logs that daily ohlcv bars data have been downloaded.
        /// </summary>
        /// <param name="entity">The identification of the down-loader class.</param>
        /// <param name="count">The number of downloaded daily ohlcv bars.</param>
        /// <param name="uri">The full request uri.</param>
        internal static void DailyOhlcvBarsDownloaded(string entity, int count, string uri)
        {
            if (logger != null)
                DailyOhlcvBarsDownloadedAction(logger, entity, count, uri, null);
        }

        private static readonly Action<ILogger, string, string, Exception> DownloadingAction =
            LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(2, nameof(Downloading)),
                "{Entity} downloading '{Uri}'");

        /// <summary>
        /// Logs that a download has been started.
        /// </summary>
        /// <param name="entity">The identification of the down-loader class.</param>
        /// <param name="uri">The full request uri.</param>
        internal static void Downloading(string entity, string uri)
        {
            if (logger != null)
                DownloadingAction(logger, entity, uri, null);
        }

        private static readonly Action<ILogger, string, string, Exception> DownloadFailedAction =
            LoggerMessage.Define<string, string>(
                LogLevel.Error,
                new EventId(3, nameof(DownloadFailed)),
                "{Entity} failed to download '{Uri}'");

        /// <summary>
        /// Logs that a download has been failed.
        /// </summary>
        /// <param name="entity">The identification of the down-loader class.</param>
        /// <param name="uri">The full request uri.</param>
        /// <param name="exception">The captured exception</param>
        internal static void DownloadFailed(string entity, string uri, Exception exception = null)
        {
            if (logger != null)
                DownloadFailedAction(logger, entity, uri, exception);
        }

        private static readonly Action<ILogger, string, Exception> NoDataDownloadedSkippingAction =
            LoggerMessage.Define<string>(
                LogLevel.Error,
                new EventId(4, nameof(NoDataDownloadedSkipping)),
                "{Entity} no data downloaded, skipping");

        /// <summary>
        /// Logs that the downloaded data is empty.
        /// </summary>
        /// <param name="entity">The identification of the down-loader class.</param>
        internal static void NoDataDownloadedSkipping(string entity)
        {
            if (logger != null)
                NoDataDownloadedSkippingAction(logger, entity, null);
        }

        private static readonly Action<ILogger, string, Exception> ExceptionHasBeenThrownAction =
            LoggerMessage.Define<string>(
                LogLevel.Error,
                new EventId(5, nameof(ExceptionHasBeenThrown)),
                "{Entity} has thrown an exception");

        /// <summary>
        /// Logs that an exception has been thrown.
        /// </summary>
        /// <param name="entity">The identification of the class.</param>
        /// <param name="exception">The thrown exception.</param>
        internal static void ExceptionHasBeenThrown(string entity, Exception exception)
        {
            if (logger != null)
                ExceptionHasBeenThrownAction(logger, entity, exception);
        }

        /// <summary>
        /// Writes a message with a <see cref="LogLevel.Debug"/> level and an optional exception.
        /// The message is enriched with a managed thread id and a date-time stamp.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="exception">An optional exception.</param>
        public static void Debug(string message, Exception exception = null)
        {
            if (exception == null)
                logger?.LogDebug(message);
            else
                logger?.LogDebug(message, exception);
        }

        /// <summary>
        /// Writes a message with a <see cref="LogLevel.Trace"/> level and an optional exception.
        /// The message is enriched with a managed thread id and a date-time stamp.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="exception">An optional exception.</param>
        public static void Trace(string message, Exception exception = null)
        {
            if (exception == null)
                logger?.LogTrace(message);
            else
                logger?.LogTrace(message, exception);
        }

        /// <summary>
        /// Writes a message with an <see cref="LogLevel.Information"/> level and an optional exception.
        /// The message is enriched with a managed thread id and a date-time stamp.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="exception">An optional exception.</param>
        public static void Information(string message, Exception exception = null)
        {
            if (exception == null)
                logger?.LogInformation(message);
            else
                logger?.LogInformation(message, exception);
        }

        /// <summary>
        /// Writes a message with a <see cref="LogLevel.Warning"/> level and an optional exception.
        /// The message is enriched with a managed thread id and a date-time stamp.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="exception">An optional exception.</param>
        public static void Warning(string message, Exception exception = null)
        {
            if (exception == null)
                logger?.LogWarning(message);
            else
                logger?.LogWarning(message, exception);
        }

        /// <summary>
        /// Writes a message with an <see cref="LogLevel.Error"/> level and an optional exception.
        /// The message is enriched with a managed thread id and a date-time stamp.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="exception">An optional exception.</param>
        public static void Error(string message, Exception exception = null)
        {
            if (exception == null)
                logger?.LogError(message);
            else
                logger?.LogError(message, exception);
        }

        /// <summary>
        /// Writes a message with an <see cref="LogLevel.Error"/> level and an optional exception.
        /// The message is enriched with a managed thread id and a date-time stamp.
        /// </summary>
        /// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args"/> array.</param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        public static void Error(string format, params object[] args)
        {
            Error(string.Format(CultureInfo.InvariantCulture, format, args));
        }

        /// <summary>
        /// Writes a message with an <see cref="LogLevel.Error"/> level if a condition is true using the specified array of objects and formatting information.
        /// The message is enriched with a managed thread id, a date-time stamp.
        /// </summary>
        /// <param name="condition">True to cause a message to be written; otherwise, false.</param>
        /// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args"/> array.</param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        public static void ErrorIf(bool condition, string format, params object[] args)
        {
            if (condition)
                Error(format, args);
        }

        /// <summary>
        /// Writes a message with a <see cref="LogLevel.Critical"/> level and an optional exception.
        /// The message is enriched with a managed thread id and a date-time stamp.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="exception">An optional exception.</param>
        public static void Critical(string message, Exception exception = null)
        {
            if (exception == null)
                logger?.LogCritical(message);
            else
                logger?.LogCritical(message, exception);
        }
    }
}

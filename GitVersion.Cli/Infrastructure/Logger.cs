﻿using Microsoft.Extensions.Logging;

namespace GitVersion.Cli.Infrastructure
{
    public class Logger : GitVersion.Infrastructure.ILogger
    {
        private readonly ILogger logger;

        public Logger(ILogger logger)
        {
            this.logger = logger;
        }

        public void LogTrace(string message, params object[] args) => logger.LogTrace(message, args);
        public void LogDebug(string message, params object[] args) => logger.LogDebug(message, args);
        public void LogInformation(string message, params object[] args) => logger.LogInformation(message, args);
        public void LogWarning(string message, params object[] args) => logger.LogWarning(message, args);
        public void LogError(string message, params object[] args) => logger.LogError(message, args);
        public void LogCritical(string message, params object[] args) => logger.LogCritical(message, args);
    }
}
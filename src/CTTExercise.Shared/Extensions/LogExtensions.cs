namespace CTTExercise.Shared.Extensions
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;

    public static class LogExtensions
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = false };

        public static string SerializeForLog(this object? source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return JsonSerializer.Serialize(source, jsonSerializerOptions);
        }

        public static void LogDebugIfEnabled(this ILogger logger, string message, params object[]? args)
        {
            logger.LogIfEnabled(LogLevel.Debug, message, args);
        }

        public static void LogInformationIfEnabled(this ILogger logger, string message, params object[]? args)
        {
            logger.LogIfEnabled(LogLevel.Information, message, args);
        }

        private static void LogIfEnabled(this ILogger logger, LogLevel logLevel, string message, params object[]? args)
        {
            if (logger.IsEnabled(logLevel))
            {
#pragma warning disable CA2254
                logger.Log(logLevel, message, SerializeArgsIfNecessary(args));
#pragma warning restore CA2254
            }
        }

        private static object[] SerializeArgsIfNecessary(params object[]? args)
        {
            if (args!.IsNullOrEmpty())
                return args ?? [];

            return args!.Select(arg =>
            {
                if (arg is null || arg.GetType().IsPrimitive || arg is string)
                    return arg!;
                return arg.SerializeForLog();
            }).ToArray();
        }
    }
}

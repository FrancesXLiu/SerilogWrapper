using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using System;

namespace SerilogWrapper
{
    public static class SerilogService
    {
        public static void ConfigureSerilog()
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.File(new CompactJsonFormatter(), "../../../logs\\log.txt", rollingInterval: RollingInterval.Day);

            Log.Logger = loggerConfig.CreateLogger();
        }

        public static void ConfigureSerilog(IConfiguration configuration)
        {
            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            Log.Logger = loggerConfig.CreateLogger();
        }

        public static void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }

        public static void OutputLog(LogLevel level, string messageTemplate, params object[] propertyValues)
        {
            switch (level)
            {
                case LogLevel.Verbose:
                    Log.Verbose(messageTemplate, propertyValues);
                    break;
                case LogLevel.Debug:
                    Log.Debug(messageTemplate, propertyValues);
                    break;
                case LogLevel.Information:
                    Log.Information(messageTemplate, propertyValues);
                    break;
                case LogLevel.Warning:
                    Log.Warning(messageTemplate, propertyValues);
                    break;
                case LogLevel.Error:
                    Log.Error(messageTemplate, propertyValues);
                    break;
                case LogLevel.Fatal:
                    Log.Fatal(messageTemplate, propertyValues);
                    break;
            }
        }
    }
}
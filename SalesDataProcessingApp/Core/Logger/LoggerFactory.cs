using Core.Helpers;
using Serilog;

namespace Core.Logger
{
    public static class LoggerFactory
    {
        public static Serilog.Core.Logger CreateConsoleLogger()
        {
            var logger = new LoggerConfiguration()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();

            return logger;
        }

        public static Serilog.Core.Logger CreateFileLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(ReadConfig.ReadSetting("LogFilePath"), outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return logger;
        }
    }
}
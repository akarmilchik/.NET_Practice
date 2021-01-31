using Core.Helpers;
using Serilog;

namespace Core.Logger
{
    public static class LoggerCreator
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
                .WriteTo.File(ReadConfig.ReadSetting("LogFilePath"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return logger;
        }

    }
}

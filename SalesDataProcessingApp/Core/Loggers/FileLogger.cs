using Core.Helpers;
using Serilog;

namespace Core.Loggers
{
    public class FileLogger
    {
        public static ILogger Create()
        {
            return new LoggerConfiguration()
                .WriteTo.File(ReadConfig.ReadSetting("LogFilePath"), outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
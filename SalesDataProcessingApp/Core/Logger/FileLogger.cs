using Core.Helpers;
using Serilog;

namespace Core.Logger
{
    public class FileLogger
    {
        public Serilog.Core.Logger CreateFileLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.File(ReadConfig.ReadSetting("LogFilePath"), outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return logger;
        }
    }
}

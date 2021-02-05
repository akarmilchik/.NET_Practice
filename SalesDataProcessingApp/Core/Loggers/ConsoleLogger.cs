using Serilog;

namespace Core.Loggers
{
    public class ConsoleLogger
    {
        public static ILogger Create()
        {
            return new LoggerConfiguration()
                   .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                   .CreateLogger();
        }
    }
}
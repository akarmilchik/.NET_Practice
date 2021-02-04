using Serilog;

namespace Core.Logger
{
    public class ConsoleLogger
    {
        public Serilog.Core.Logger Create()
        {
            var logger = new LoggerConfiguration()
                   .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                   .CreateLogger();

            return logger;
        }
    }
}

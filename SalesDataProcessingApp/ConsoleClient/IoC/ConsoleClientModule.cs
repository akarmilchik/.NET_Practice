using Autofac;
using Core.Loggers;
using Serilog;

namespace ConsoleClient.IoC
{
    public class ConsoleClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(ConsoleLogger.Create()).As<ILogger>();
        }
    }
}
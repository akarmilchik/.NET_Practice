using Autofac;
using Core.IoC;
using DAL.IoC;

namespace ConsoleClient.IoC
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<DALModule>();
            builder.RegisterModule<ConsoleClientModule>();

            return builder.Build();
        }
    }
}
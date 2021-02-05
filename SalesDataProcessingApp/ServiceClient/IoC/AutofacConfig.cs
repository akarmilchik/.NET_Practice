using Autofac;
using Core.IoC;
using DAL.IoC;

namespace ServiceApp.IoC
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<DALModule>();
            builder.RegisterModule<ServiceClientModule>();

            return builder.Build();
        }
    }
}
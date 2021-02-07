using Autofac;
using DAL.IoC;

namespace Core.IoC
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<DALModule>();

            return builder.Build();
        }
    }
}
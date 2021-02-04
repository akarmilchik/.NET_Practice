using Autofac;
using Core.Interfaces;
using Core.Logger;
using Core.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Serilog;

namespace Core.Extensions
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule

            builder.RegisterType<DataContext>().As<DataContext>();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<FileLogger>().As<ILogger>();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<DataService>().As<IDataService>();
            builder.RegisterType<ParseService>().As<IParseService>();

            return  builder.Build();
        }
    }
}

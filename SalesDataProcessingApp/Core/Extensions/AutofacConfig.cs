using Autofac;
using Core.Interfaces;
using Core.Services;
using DAL.Interfaces;
using DAL.Repositories;

namespace Core.Extensions
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();           

            // регистрируем споставление типов
            builder.RegisterType<DataRepository>().As<IGenericRepository>();

            builder.RegisterType<DataService>().As<IDataService>();

            builder.RegisterType<ParseService>().As<IParseService>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            return builder.Build();
        }
    }
}

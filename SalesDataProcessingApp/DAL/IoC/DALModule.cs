using Autofac;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.IoC
{
    public class DALModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();
        }
    }
}
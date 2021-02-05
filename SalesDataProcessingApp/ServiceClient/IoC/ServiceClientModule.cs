using Autofac;
using Core.Helpers;

namespace ServiceApp.IoC
{
    public class ServiceClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileWatchService>().AsSelf().WithParameter("filesFolderPath", ReadConfig.ReadSetting("DataFilesPath"));
        }
    }
}
using Autofac;
using Core.FileProcessing;
using Core.Helpers;
using Core.Interfaces;
using Core.Loggers;
using Core.Services;
using Serilog;

namespace Core.IoC
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ParseService>().As<IParseService>();
            builder.RegisterType<DataService>().As<IDataService>();
            builder.RegisterType<FileHandler>().As<IFileHandler>().WithParameter("filesFolderPath", ReadConfig.ReadSetting("DataFilesPath"));
            
            builder.RegisterInstance(FileLogger.Create()).As<ILogger>();
        }
    }
}
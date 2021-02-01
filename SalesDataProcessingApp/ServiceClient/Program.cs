using Core.Helpers;
using Core.Interfaces;
using Core.Logger;
using Core.Services;
using DAL;
using Serilog.Core;
using System;
using System.ServiceProcess;

namespace ServiceApp
{
    static class Program
    {
        public static DataContext context;

        public static Logger logger;

        public static IDataService dataService;

        public static FileWatchService fileWatchService;

        static void Main(string[] args)
        {
            InitLogger();

            InitDataService();

            logger.Information("Hello effective manager! Starting service client...");

            if (Environment.UserInteractive)
            {
                //work as console

                fileWatchService = new FileWatchService(ReadConfig.ReadSetting("DataFilesPath"), logger, dataService);

                fileWatchService.WorkAsConsole(args);
            }
            else
            {
                // work as service

                ServiceBase[] ServicesToRun;

                ServicesToRun = new ServiceBase[]
                {
                    new FileWatchService(ReadConfig.ReadSetting("DataFilesPath"), logger, dataService)
                };

                ServiceBase.Run(ServicesToRun);
            }
        }

        public static void CreateContext()
        {
            context = new DataContext();
        }

        public static void InitLogger()
        {
            logger = LoggerFactory.CreateFileLogger();
        }

        public static void InitDataService()
        {
            dataService = new DataService(context, logger);
        }
    }
}

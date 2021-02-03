using Core.Helpers;
using Core.Interfaces;
using Core.Logger;
using Core.Services;
using DAL;
using DAL.Helpers;
using Serilog.Core;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

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
            int i = 0;

            bool isWorking = true;

            CreateContext();

            InitLogger();

            InitData.InitializeData(context);

            InitDataService();

            logger.Information("Hello effective manager! Starting service client...");

            if (Environment.UserInteractive)
            {
                //work as console

                while (isWorking)
                {
                    logger.Information($"Searching for files... [{i++}]");

                    var searchingTask = new Task(() => SearchFiles(args));

                    searchingTask.Start();

                    searchingTask.Wait();

                    logger.Information("Exit from DOWN task to main.");
                }
            }
            else
            {
                // work as service

                while (isWorking)
                {
                    logger.Information($"Searching for files... [{i++}]");

                    var searchingTask = new Task(() => SearchFilesService());

                    searchingTask.Start();

                    searchingTask.Wait();

                    logger.Information("Exit from DOWN task to main.");
                }
            }
        }

        public static void SearchFiles(string[] args)
        {
            fileWatchService = new FileWatchService(ReadConfig.ReadSetting("DataFilesPath"), logger, dataService);

            fileWatchService.WorkAsConsole(args);

            Thread.Sleep(5000);

            if (Thread.CurrentThread.IsAlive)
            {
                fileWatchService.StopAsConsole();
            }
        }

        public static void SearchFilesService()
        {
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                    new FileWatchService(ReadConfig.ReadSetting("DataFilesPath"), logger, dataService)
            };

            ServiceBase.Run(ServicesToRun);

            Thread.Sleep(5000);

            if (Thread.CurrentThread.IsAlive)
            {
                ServicesToRun[0].Stop();
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

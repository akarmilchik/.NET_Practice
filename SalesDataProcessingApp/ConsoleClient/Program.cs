using ConsoleClient.Helpers;
using Core;
using Core.Helpers;
using Core.Interfaces;
using Core.Logger;
using Core.Services;
using DAL;
using Serilog.Core;
using System;

namespace ConsoleClient
{
    class Program
    {
        public static DataContext context;

        public static IDataService dataService;

        public static FileWatcher fileWatcher;

        public static Logger logger;

        static void Main(string[] args)
        {
            bool isWorking = true;

            CreateContext();

            InitLogger();

            InitData.InitializeData(context, logger);

            InitDataService();

            InitWatcher();

            logger.Information("Hello effective manager! Starting console client...");

            while (isWorking)
            {
                fileWatcher.StartWatch(dataService);

                logger.Information("Searching for files... To stop, please input \"stop\":");

                var input = Console.ReadLine().Trim();

                if (input == "stop")
                {
                    isWorking = false;
                }
            }

            fileWatcher.StopWatch();

            logger.Information("Exit...");
        }

        public static void CreateContext()
        {
            context = new DataContext();
        }

        public static void InitLogger()
        {
            logger = LoggerCreator.CreateConsoleLogger();
        }

        public static void InitDataService()
        {
            dataService = new DataService(context, logger);
        }

        public static void InitWatcher()
        {
            try
            {
                fileWatcher = new FileWatcher(ReadConfig.ReadSetting("DataFilesPath"), logger);
            }
            catch (Exception e)
            {
                logger.Error($"Error initializing watcher: {e.Message}.");
            }
        }
    }
}

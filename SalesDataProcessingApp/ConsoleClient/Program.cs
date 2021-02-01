using ConsoleCLient.Helpers;
using Core.FileProcessing;
using Core.Helpers;
using Core.Interfaces;
using Core.Logger;
using Core.Services;
using DAL;
using Serilog.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Program
    {
        public static DataContext context;

        public static IDataService dataService;

        public static FileWatcher fileWatcher;

        public static Logger logger;

        public static void Main()
        {
            bool isWorking = true;

            int i = 0;

            CreateContext();

            InitLogger();

            InitData.InitializeData(context, logger);

            InitDataService();

            InitWatcher();

            logger.Information("Hello effective manager! Starting console client...");

            while (isWorking)
            {
                logger.Information($"Searching for files... [{i++}]");

                var searchingTask = new Task(() => SearchFiles());

                searchingTask.Start();

                searchingTask.Wait();

                if (i == 100)
                {
                    isWorking = false;
                }    
            }

            fileWatcher.StopWatch();

            logger.Information("Exit...");
        }

        public static void SearchFiles()
        {
            Thread.Sleep(5000);

            fileWatcher.StartWatch();
        }

        public static void CreateContext()
        {
            context = new DataContext();
        }

        public static void InitLogger()
        {
            logger = LoggerFactory.CreateConsoleLogger();
        }

        public static void InitDataService()
        {
            dataService = new DataService(context, logger);
        }

        public static void InitWatcher()
        {
            try
            {
                fileWatcher = new FileWatcher(ReadConfig.ReadSetting("DataFilesPath"), logger, dataService);
            }
            catch (Exception e)
            {
                logger.Error($"Error initializing watcher: {e.Message}.");
            }
        }
    }
}

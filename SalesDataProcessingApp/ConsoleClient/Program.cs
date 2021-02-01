using ConsoleClient.Helpers;
using Core;
using Core.Helpers;
using Core.Interfaces;
using Core.Logger;
using Core.Services;
using Core.FileProcessing;
using DAL;
using Serilog.Core;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleClient
{
    public class Program
    {
        public static DataContext context;

        public static IDataService dataService;

        public static FileWatcher fileWatcher;

        public static Logger logger;

        static void Main(string[] args)
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

            fileWatcher.StartWatch(dataService);
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
                fileWatcher = new FileWatcher(ReadConfig.ReadSetting("DataFilesPath"), logger);
            }
            catch (Exception e)
            {
                logger.Error($"Error initializing watcher: {e.Message}.");
            }
        }
    }
}

using Core.FileProcessing;
using Core.Helpers;
using Core.Logger;
using DAL;
using DAL.Helpers;
using Serilog.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Program
    {
        public static DataContext context;

        public static FileWatcher fileWatcher;

        public static Logger logger;

        public static void Main()
        {
            bool isWorking = true;

            int i = 0;

            InitContext();

            InitLogger();

            InitWatcher();

            logger.Information("Hello effective manager! Starting console client...");

            while (isWorking)
            {
                logger.Information($"Searching for files... [{i++}]");

                var searchingTask = new Task(() => SearchFiles());

                searchingTask.Start();

                searchingTask.Wait();

                logger.Information("Exit from DOWN task to main.");   
            }
        }

        public static void SearchFiles()
        {
            fileWatcher.StartWatch();

            Thread.Sleep(5000);

            if (Thread.CurrentThread.IsAlive)
            {
                fileWatcher.StopWatch();
            }
        }

        public static void InitContext()
        {
            context = new DataContext();

            InitData.InitializeData(context);

            context.Dispose();
        }

        public static void InitLogger()
        {
            logger = LoggerFactory.CreateConsoleLogger();
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

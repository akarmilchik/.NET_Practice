using Autofac;
using Core.Extensions;
using Core.FileProcessing;
using Core.Helpers;
using Core.Interfaces;
using DAL;
using DAL.Interfaces;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Program
    {
        public static DataContext _context;
        public static FileWatcher fileWatcher;
        public static ILogger _logger;
        public static IParseService _parseService;
        public static IRepository _repository;
        public static IContainer container;

        public Program(ILogger logger, IParseService parseService, IRepository repository, DataContext context)
        {
            _logger = logger;
            _context = context;
            _parseService = parseService;
            _repository = repository;
        }

        public static void Main()
        {
            int i = 0;
            bool isWorking = true;

            InitContainer();
            InitWatcher();

            _logger.Information("Hello effective manager! Starting console client...");

            while (isWorking)
            {
                _logger.Information($"Searching for files... [{i++}]");

                var searchingTask = new Task(() => SearchFiles());

                searchingTask.Start();

                searchingTask.Wait();

                _logger.Information("Exit from DOWN task to main.");
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

        public static void InitContainer()
        {
            container = AutofacConfig.ConfigureContainer();
        }

        public static void InitWatcher()
        {
            try
            {
                fileWatcher = new FileWatcher(ReadConfig.ReadSetting("DataFilesPath"), _logger, _parseService, _repository);
            }
            catch (Exception e)
            {
                _logger.Error($"Error initializing watcher: {e.Message}.");
            }
        }
    }
}

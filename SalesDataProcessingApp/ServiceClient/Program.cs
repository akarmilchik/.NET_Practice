using Autofac;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using DAL;
using DAL.Interfaces;
using Serilog;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApp
{
    class Program
    {
        public static DataContext context;
        public static ILogger _logger;
        public static IParseService _parseService;
        public static IRepository _repository;
        public static IContainer container;
        public static FileWatchService fileWatchService;

        public Program(ILogger logger, IParseService parseService, IRepository repository)
        {
            _logger = logger;
            _parseService = parseService;
            _repository = repository;
        }

        static void Main(string[] args)
        {
            int i = 0;

            bool isWorking = true;

            InitContainer();

            _logger.Information("Hello effective manager! Starting service client...");

            if (Environment.UserInteractive)
            {
                //work as console

                while (isWorking)
                {
                    _logger.Information($"Searching for files... [{i++}]");

                    var searchingTask = new Task(() => SearchFiles(args));

                    searchingTask.Start();

                    searchingTask.Wait();

                    _logger.Information("Exit from DOWN task to main.");
                }
            }
            else
            {
                // work as service

                while (isWorking)
                {
                    _logger.Information($"Searching for files... [{i++}]");

                    var searchingTask = new Task(() => SearchFilesService());

                    searchingTask.Start();

                    searchingTask.Wait();

                    _logger.Information("Exit from DOWN task to main.");
                }
            }
        }

        public static void SearchFiles(string[] args)
        {
            fileWatchService = new FileWatchService(ReadConfig.ReadSetting("DataFilesPath"), _logger, _parseService, _repository);

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
                new FileWatchService(ReadConfig.ReadSetting("DataFilesPath"), _logger, _parseService, _repository)
            };

            ServiceBase.Run(ServicesToRun);

            Thread.Sleep(5000);

            if (Thread.CurrentThread.IsAlive)
            {
                ServicesToRun[0].Stop();
            }
        }

        public static void InitContainer()
        {
            container = AutofacConfig.ConfigureContainer();
        }
    }
}

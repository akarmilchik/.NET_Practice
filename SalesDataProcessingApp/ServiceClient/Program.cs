using Autofac;
using Serilog;
using ServiceApp.IoC;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApp
{
    class Program
    {
        public static ILogger _logger;
        public static IContainer container;
        public static FileWatchService fileWatchService;

        static void Main(string[] args)
        { 
            int i = 0;

            bool isWorking = true;

            Task searchingTask;

            InitContainer();

            _logger = container.Resolve<ILogger>();

            _logger.Information("Hello effective manager! Starting service client...");
            
            while (isWorking)
            {
                _logger.Information($"Searching for files... [{i++}]");

                if (Environment.UserInteractive)
                {
                    searchingTask = new Task(() => SearchFiles(args));
                }
                else 
                {
                    searchingTask = new Task(() => SearchFilesService());
                }

                searchingTask.Start();

                searchingTask.Wait();

                _logger.Information("Exit from DOWN task to main.");
            }
        }

        public static void SearchFiles(string[] args)
        {
            fileWatchService = container.Resolve<FileWatchService>();

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

            fileWatchService = container.Resolve<FileWatchService>();

            ServicesToRun = new ServiceBase[]
            {
                fileWatchService
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
using Autofac;
using ConsoleClient.IoC;
using Core.Interfaces;
using DAL.Helpers;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Program
    {
        public static IFileHandler fileHandler;
        public static ILogger _logger;
        public static IContainer container;

        public static void Main()
        {
            int i = 0;

            bool isWorking = true;

            InitContainer();

            InitData.InitializeDatatable();

            _logger = container.Resolve<ILogger>();

            _logger.Information("Hello effective manager! Starting console client...");

            while (isWorking)
            {
                _logger.Information($"Searching for files... [{i++}]");

                var searchingTask = new Task(() => SearchFiles());

                searchingTask.Start();

                searchingTask.Wait();

                _logger.Information("Exit from inner task to main.");
            }
        }

        private static void SearchFiles()
        {
            fileHandler = container.Resolve<IFileHandler>();

            fileHandler.Start();

            Thread.Sleep(5000);

            if (Thread.CurrentThread.IsAlive)
            {
                fileHandler.Stop();
            }
        }

        private static void InitContainer()
        {
            container = AutofacConfig.ConfigureContainer();
        }
    }
}
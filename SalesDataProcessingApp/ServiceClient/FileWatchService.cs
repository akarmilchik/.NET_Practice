using Core.FileProcessing;
using Core.Interfaces;
using Serilog.Core;
using System;
using System.ServiceProcess;
using System.Threading;

namespace ServiceApp
{
    public partial class FileWatchService : ServiceBase
    {
        private FileHandler _fileHandler;

        private readonly Logger _logger;

        public IDataService _dataService;

        private readonly string _filesFolderPath;

        public FileWatchService(string filesFolderPath, Logger logger, IDataService dataService)
        {
            InitializeComponent();

            this.CanStop = true;

            this.CanPauseAndContinue = true;

            this.AutoLog = true;

            _filesFolderPath = filesFolderPath;

            _dataService = dataService;

            _logger = logger;
        }

        protected override void OnStart(string[] args)
        {
            _fileHandler = new FileHandler(_filesFolderPath, _logger, _dataService);

            Thread loggerThread = new Thread(new ThreadStart(_fileHandler.Start));

            loggerThread.Start();

            Console.WriteLine("Thread start");
        }

        protected override void OnStop()
        {
            _fileHandler.Stop();

            Thread.Sleep(1000);
        }

        internal void WorkAsConsole(string[] args)
        {
            bool isWorking = true;

            while (isWorking)
            {
                this.OnStart(args);

                _logger.Information("Searching for files...");

                var input = Console.ReadLine().Trim();

                if (input == "stop")
                { 
                    isWorking = false; 
                }
            }

            this.OnStop();

            _logger.Information("Exit...");
        }
    }
}

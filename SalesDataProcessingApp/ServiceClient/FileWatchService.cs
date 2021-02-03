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
            _logger.Information("Start watcher.");

            _fileHandler = new FileHandler(_filesFolderPath, _logger, _dataService);

            _fileHandler.Start();
        }

        protected override void OnStop()
        {
            _fileHandler.Stop();

            _logger.Information("Stop watcher.");
        }

        internal void WorkAsConsole(string[] args)
        {
            this.OnStart(args);
        }


        internal void StopAsConsole()
        {
            this.OnStop();
        }

    }
}

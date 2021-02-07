using Core.Interfaces;
using Serilog;
using System.ServiceProcess;

namespace ServiceApp
{
    public partial class FileWatchService : ServiceBase
    {
        private readonly IFileHandler _fileHandler;
        private readonly ILogger _logger;

        public FileWatchService(ILogger logger, IFileHandler fileHandler)
        {
            InitializeComponent();

            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
            _logger = logger;
            _fileHandler = fileHandler;
        }

        protected override void OnStart(string[] args)
        {
            _fileHandler.Start();

            _logger.Information("Start watcher.");
        }

        protected override void OnStop()
        {
            _fileHandler.Stop();

            _logger.Information("Stop watcher.");
        }

        internal void WorkAsConsole(string[] args)
        {
            OnStart(args);
        }

        internal void StopAsConsole()
        {
            OnStop();
        }
    }
}
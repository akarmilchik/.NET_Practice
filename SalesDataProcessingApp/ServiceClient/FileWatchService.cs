using Core.FileProcessing;
using Core.Interfaces;
using DAL.Interfaces;
using Serilog;
using System.ServiceProcess;

namespace ServiceApp
{
    public partial class FileWatchService : ServiceBase
    {
        private FileHandler _fileHandler;
        private readonly ILogger _logger;
        private readonly IParseService _parseService;
        private readonly IRepository _repository;
        private readonly string _filesFolderPath;

        public FileWatchService(string filesFolderPath, ILogger logger, IParseService parseService, IRepository repository)
        {
            InitializeComponent();

            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;

            _filesFolderPath = filesFolderPath;
            _logger = logger;
            _parseService = parseService;
            _repository = repository;
        }

        protected override void OnStart(string[] args)
        {
            _logger.Information("Start watcher.");

            _fileHandler = new FileHandler(_filesFolderPath, _logger, _parseService, _repository);

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
using Core.Interfaces;
using DAL.Interfaces;
using Serilog;

namespace Core.FileProcessing
{
    public class FileWatcher
    {
        private readonly ILogger _logger;
        private readonly IParseService _parseService;
        private readonly IRepository _repository;
        private readonly string _filesFolderPath;
        private FileHandler _fileHandler;

        public FileWatcher(string filesFolderPath, ILogger logger, IParseService parseService, IRepository repository)
        {
            _filesFolderPath = filesFolderPath;
            _logger = logger;
            _parseService = parseService;
            _repository = repository;
        }

        public void StartWatch()
        {
            _logger.Information("Start watcher.");

            _fileHandler = new FileHandler(_filesFolderPath, _logger, _parseService, _repository);

            _fileHandler.Start();            
        }

        public void StopWatch()
        {
            _fileHandler.Stop();

            _logger.Information("Stop watcher.");
        }
    }
}

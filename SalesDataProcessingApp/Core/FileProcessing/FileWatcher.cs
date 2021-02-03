namespace Core.FileProcessing
{
    public class FileWatcher
    {
        private readonly Serilog.Core.Logger _logger;

        private readonly string _filesFolderPath;

        private FileHandler _fileHandler;

        public FileWatcher(string filesFolderPath, Serilog.Core.Logger logger)
        {
            _filesFolderPath = filesFolderPath;

            _logger = logger;
        }

        public void StartWatch()
        {
            _logger.Information("Start watcher.");

            _fileHandler = new FileHandler(_filesFolderPath, _logger);

            _fileHandler.Start();            
        }

        public void StopWatch()
        {
            _fileHandler.Stop();

            _logger.Information("Stop watcher.");
        }
    }
}

using Core.Interfaces;
using Core.Services;
using System.Threading;

namespace Core
{
    public class FileWatcher
    {
        private readonly Serilog.Core.Logger _logger;

        public FileHandler _fileHandler;

        private string _filePath;

        public FileWatcher(string filePath, Serilog.Core.Logger logger)
        {
            _filePath = filePath;

            _logger = logger;
        }

        public void StartWatch(IDataService dataService)
        {
            _fileHandler = new FileHandler(_filePath, _logger, dataService);

            Thread handlerThread = new Thread(_fileHandler.Start);

            handlerThread.Start();

            _logger.Information("Start watcher.");
        }

        public void StopWatch()
        {
            _fileHandler.Stop();

            _logger.Information("Stop watcher.");
        }
    }
}

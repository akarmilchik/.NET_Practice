using Core.Interfaces;
using System.Threading.Tasks;

namespace Core.FileProcessing
{
    public class FileWatcher
    {
        private readonly Serilog.Core.Logger _logger;

        public FileHandler _fileHandler;

        private string _filesFolderPath;

        public FileWatcher(string filesFolderPath, Serilog.Core.Logger logger)
        {
            _filesFolderPath = filesFolderPath;

            _logger = logger;
        }

        public void StartWatch(IDataService dataService)
        {
            _fileHandler = new FileHandler(_filesFolderPath, _logger, dataService);

            Task handlerTask = new Task(_fileHandler.Start, TaskCreationOptions.AttachedToParent);

            handlerTask.Start();

            _logger.Information("Start watcher.");
        }

        public void StopWatch()
        {
            _fileHandler.Stop();

            _logger.Information("Stop watcher.");
        }
    }
}

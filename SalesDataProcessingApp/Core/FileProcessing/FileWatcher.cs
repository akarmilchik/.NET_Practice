using Core.Interfaces;
using System.Threading.Tasks;

namespace Core.FileProcessing
{
    public class FileWatcher
    {
        private readonly Serilog.Core.Logger _logger;

        private readonly string _filesFolderPath;

        private readonly IDataService _dataService;

        private FileHandler _fileHandler;

        public FileWatcher(string filesFolderPath, Serilog.Core.Logger logger, IDataService dataService)
        {
            _filesFolderPath = filesFolderPath;

            _dataService = dataService;

            _logger = logger;
        }

        public void StartWatch()
        {
            _logger.Information("Start watcher.");

            _fileHandler = new FileHandler(_filesFolderPath, _logger, _dataService);

            //Task handlerTask = new Task(_fileHandler.Start, TaskCreationOptions.AttachedToParent);

            //handlerTask.Start();

            _fileHandler.Start();            
        }

        public void StopWatch()
        {
            _fileHandler.Stop();

            _logger.Information("Stop watcher.");
        }
    }
}

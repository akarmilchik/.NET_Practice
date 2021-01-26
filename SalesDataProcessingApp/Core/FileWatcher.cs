using Core.Interfaces;
using System.Threading;

namespace Core
{
    public class FileWatcher
    {
        public FileHandler _fileHandler;

        private string _filePath;

        public FileWatcher(string filePath)
        {
            _filePath = filePath;
        }

        public void StartWatch(IDataService dataService)
        {
            _fileHandler = new FileHandler(_filePath, dataService);

            Thread loggerThread = new Thread(new ThreadStart(_fileHandler.Start));

            loggerThread.Start();
        }

        public void StopWatch()
        {
            _fileHandler.Stop();
        }
    }
}

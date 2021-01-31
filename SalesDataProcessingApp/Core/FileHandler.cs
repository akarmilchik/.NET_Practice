using Core.Interfaces;
using Core.Services;
using System;
using System.IO;

namespace Core
{
    public class FileHandler
    {
        private FileSystemWatcher watcher;

        private readonly Serilog.Core.Logger _logger;

        private readonly IDataService _dataService;

        public FileHandler(string filesPath, Serilog.Core.Logger logger, IDataService dataService)
        {
            watcher = new FileSystemWatcher(filesPath);

            _logger = logger;

            _dataService = dataService;

            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;

            watcher.Created -= Watcher_Created;
            watcher.Changed -= Watcher_Changed;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "updated";

            string filePath = e.FullPath;

            ProcessFile(filePath, fileEvent);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "parsed";

            string filePath = e.FullPath;

            ProcessFile(filePath, fileEvent);
        }

        private void ProcessFile(string filePath, string fileEvent)
        {

            //конструкция синхронизации, залочить файл

            _logger.Information("Start process file in handler.");

            _dataService.ProcessFile(filePath);
        }
    }
}

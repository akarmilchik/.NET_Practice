using Core.Interfaces;
using System;
using System.IO;
using System.Threading;

namespace Core
{
    public class FileHandler
    {
        private FileSystemWatcher watcher;

        private bool enabled = true;

        private static IDataService _dataService;

        public FileHandler(string filesPath, IDataService dataService)
        {
            watcher = new FileSystemWatcher(filesPath);

            _dataService = dataService;

            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;

            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;

            enabled = false;

            watcher.Created -= Watcher_Created;
            watcher.Changed -= Watcher_Changed;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "changed";

            string filePath = e.FullPath;

            ProcessRecord(fileEvent, filePath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";

            string filePath = e.FullPath;

            ProcessRecord(fileEvent, filePath);
        }

        private string ProcessRecord(string fileEvent, string filePath)
        {
            _dataService.ProcessFiles(filePath);

            Console.WriteLine(string.Format($"{DateTime.Now.ToString("G")} file {filePath} was been {fileEvent}"));

            return filePath;
        }
    }
}

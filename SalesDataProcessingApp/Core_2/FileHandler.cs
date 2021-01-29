using Core.Interfaces;
using System;
using System.IO;
using System.Threading;

namespace Core_2
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
            //Thread fileProcessThread = new Thread(new ParameterizedThreadStart(_dataService.ProcessFile()));

            //fileProcessThread.Start(filePath);

            Console.WriteLine("Start reading file thread!");

            Console.WriteLine(string.Format($"{DateTime.Now.ToString("G")} file {filePath} was been {fileEvent} in database"));
        }
    }
}

﻿using ServiceApp.Helpers;
using System;
using System.IO;
using System.Threading;

namespace ServiceApp
{
    class Logger
    {
        FileSystemWatcher watcher;

        object obj = new object();

        bool enabled = true;

        public Logger()
        {
            watcher = new FileSystemWatcher(ReadConfig.ReadSetting("DataFilesPath"));

            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;

            while (enabled)
            {
                Thread.Sleep(5000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;

            enabled = false;

            watcher.Deleted -= Watcher_Deleted;
            watcher.Created -= Watcher_Created;
            watcher.Changed -= Watcher_Changed;
            watcher.Renamed -= Watcher_Renamed;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "renamed to " + e.FullPath;

            string filePath = e.OldFullPath;

            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "changed";

            string filePath = e.FullPath;

            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";

            string filePath = e.FullPath;

            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "deleted";

            string filePath = e.FullPath;

            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter("D:\\templog.txt", true))
                {
                    writer.WriteLine(string.Format($"{DateTime.Now.ToString("G")} file {filePath} was been {fileEvent}"));

                    writer.Flush();
                }
            }
        }
        
    }
}

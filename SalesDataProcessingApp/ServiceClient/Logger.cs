using ServiceApp.Helpers;
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

            watcher.Created += Watcher_Created;

            watcher.Changed += Watcher_Changed;
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

            watcher.Created -= Watcher_Created;

            watcher.Changed -= Watcher_Changed;
        }


        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "changed";

            string filePath = e.FullPath;

            ProcessFile(fileEvent, filePath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";

            string filePath = e.FullPath;

            ProcessFile(fileEvent, filePath);
        }

        private void ProcessFile(string fileEvent, string filePath)
        {
            lock (obj)
            {
                //work with context


                using (StreamWriter writer = new StreamWriter(ReadConfig.ReadSetting("LogFilePath"), true))
                {
                    writer.WriteLine(string.Format($"{DateTime.Now.ToString("G")} file {filePath} was been {fileEvent}"));

                    writer.Flush();
                }
            }
        }
    }
}

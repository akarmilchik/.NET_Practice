﻿using Core.Extensions;
using Core.Interfaces;
using DAL.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.FileProcessing
{
    public class FileHandler
    {
        private readonly FileSystemWatcher _watcher;

        private readonly Serilog.Core.Logger _logger;

        private readonly IDataService _dataService;

        public FileHandler(string filesFolderPath, Serilog.Core.Logger logger, IDataService dataService)
        {
            _watcher = new FileSystemWatcher(filesFolderPath);

            _logger = logger;

            _dataService = dataService;

            _watcher.Created += Watcher_Created;

            _watcher.Changed += Watcher_Changed;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;

            _logger.Information("Start handler.");
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;

            _watcher.Created -= Watcher_Created;

            _watcher.Changed -= Watcher_Changed;

            _logger.Information("Stop handler.");
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var file = new FileModel() { Name = e.Name, Path = e.FullPath };

            ProcessFile(file);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            var file = new FileModel() { Name = e.Name, Path = e.FullPath };

            ProcessFile(file);
        }

        private void ProcessFile(FileModel file)
        {
            ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

            using (locker.Read())
            {
                _logger.Information($"Lock file {file.Name}.");

                Task processFileTask = new Task(() => _dataService.ProcessFile(file.Path));

                processFileTask.Start();

                _logger.Information($"Task id:{processFileTask.Id};  File:{file.Name}");
            }
        }
    }
}
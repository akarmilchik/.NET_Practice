using Core.Extensions;
using Core.Interfaces;
using Core.Services;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using Serilog;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.FileProcessing
{
    public class FileHandler
    {
        private readonly FileSystemWatcher _watcher;
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly IParseService _parseService;
        private DataContext _context;

        public FileHandler(string filesFolderPath, ILogger logger, IParseService parseService, IRepository repository)
        {
            _watcher = new FileSystemWatcher(filesFolderPath);
            _logger = logger;
            _parseService = parseService;
            _repository = repository;
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


                //here we need to create a new context and datasrvice for this context
                var _context = new DataContext();

                var _dataService = new DataService(_context, _parseService, _repository);

                Task processFileTask = new Task(() => _dataService.ProcessFile(file.Path));

                processFileTask.Start();

                _logger.Information($"Task id:{processFileTask.Id};  File:{file.Name}");
            }

            _logger.Information($"Unlock file {file.Name}.");
        }
    }
}

using System;
using System.ServiceProcess;
using System.Threading;

namespace ServiceApp
{
    public partial class FileWatchService : ServiceBase
    {
        Logger logger;

        public FileWatchService(string[] Args)
        {
            InitializeComponent();

            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {

            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);

            Console.WriteLine("To stop ManagerFileWatchService input \"stop\":");

            var res = Console.ReadLine().Trim();

            while ((res != "stop"))
            {
                Console.WriteLine("Wrong input, try again:");

                res = Console.ReadLine().Trim();
            }

            this.OnStop();
        }
    }
}

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

            Console.WriteLine("Thread start");
        }

        protected override void OnStop()
        {
            logger.Stop();

            Thread.Sleep(1000);
        }

        internal void WorkAsConsole(string[] args)
        {
            bool isWorking = true;

            while (isWorking)
            {
                this.OnStart(args);

                Console.WriteLine("Searching for files... To stop, please input \"stop\":");

                var input = Console.ReadLine().Trim();

                if (input == "stop")
                { 
                    isWorking = false; 
                }
            }

            this.OnStop();

            Console.WriteLine("Exit...");
        }
    }
}

using System;
using System.ServiceProcess;

namespace ServiceApp
{
    static class Program
    {
        static void Main(string[] args)
        {

            if (Environment.UserInteractive)
            {
                FileWatchService watchService = new FileWatchService(args);

                watchService.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;

                ServicesToRun = new ServiceBase[]
                {
                    new FileWatchService(null)
                };

                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}

using System;
using System.ServiceProcess;

namespace ServiceApp
{
    static class Program
    {
        static void Main(string[] args)
        {

            //CreateHostBuilder(args).Build().Run();

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

        /*public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<SoteriaDbContext>();
                    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Soteria;Trusted_Connection=True;");//,
                    services.AddScoped<SoteriaDbContext>(s => new SoteriaDbContext(optionsBuilder.Options));

                    services.AddHostedService<Worker>();
                });

            return host;
        }*/
    }
}

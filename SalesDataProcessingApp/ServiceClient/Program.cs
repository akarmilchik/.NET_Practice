using Microsoft.Extensions.Hosting;
using System;
using System.ServiceProcess;

namespace ServiceApp
{
    static class Program
    {
        static void Main(string[] args)
        {

           // CreateHostBuilder(args).Build().Run();

            if (Environment.UserInteractive)
            {
                FileWatchService watchService = new FileWatchService(args);

                watchService.WorkAsConsole(args);
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
        /*
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Soteria;Trusted_Connection=True;");
                    services.AddScoped<SoteriaDbContext>(s => new SoteriaDbContext(optionsBuilder.Options));

                    services.AddHostedService<Worker>();
                });

            return host;
        }*/
    }
}

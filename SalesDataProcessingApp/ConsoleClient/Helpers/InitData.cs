using ConsoleClient;
using DAL;

namespace ConsoleCLient.Helpers
{
    public static class InitData
    {
        public static void InitializeData(DataContext context, Serilog.Core.Logger logger)
        {
            context.Database.Exists();

            var seeder = new DataSeeder(context, logger);

            seeder.SeedData();
        }
    }
}

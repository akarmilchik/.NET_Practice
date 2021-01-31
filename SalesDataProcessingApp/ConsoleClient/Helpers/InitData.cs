using DAL;
using Serilog.Core;

namespace ConsoleClient.Helpers
{
    public static class InitData
    {
        public static void InitializeData(DataContext context, Logger logger)
        {
            context.Database.Exists();

            var seeder = new DataSeeder(context, logger);

            seeder.SeedData();
        }
    }
}

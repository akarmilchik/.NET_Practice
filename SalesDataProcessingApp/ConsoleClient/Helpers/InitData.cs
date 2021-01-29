using DAL;

namespace ConsoleClient.Helpers
{
    public static class InitData
    {
        public static void InitializeData(DataContext context)
        {
            var seeder = new DataSeeder(context);

            seeder.SeedData();
        }
    }
}

using ATS.DAL;

namespace ATS.Helpers
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

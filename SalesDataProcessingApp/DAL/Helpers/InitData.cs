namespace DAL.Helpers
{
    public static class InitData
    {
        public static void InitializeData(DataContext context)
        {
            context.Database.Exists();

            var seeder = new DataSeeder(context);

            seeder.SeedData();
        }
    }
}
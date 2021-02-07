namespace DAL.Helpers
{
    public static class InitData
    {
        public static void InitializeDatatable()
        {
            var context = new DataContext();

            context.Database.CreateIfNotExists();

            var seeder = new DataSeeder(context);

            seeder.SeedData();
        }
    }
}
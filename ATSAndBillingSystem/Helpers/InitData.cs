using ATS.DAL;

namespace ATS.Helpers
{
    public class InitData
    {
        public void InitializeData(DataContext context)
        {
            var seeder = new DataSeeder(context);
            seeder.SeedData();
        }
    }
}

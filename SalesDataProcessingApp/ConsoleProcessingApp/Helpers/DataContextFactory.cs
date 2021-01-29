using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleProcessingApp.Helpers
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseSqlServer(ReadConfig.ReadSetting("DBConnectionString"));

            //optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = ProductsSalesDb; Trusted_Connection = True; MultipleActiveResultSets = true;");
            
            return new DataContext(optionsBuilder.Options);
        }
    }
}

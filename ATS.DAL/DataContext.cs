using ATS.DAL.Helpers;
using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using ATS.DAL.Models.TariffPlans;
using Microsoft.EntityFrameworkCore;

namespace ATS.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<SecondMinuteTariffPlan> TariffPlans { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Station> Stations { get; set; }

        public override int SaveChanges() => base.SaveChanges();
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"{ReadConfig.ReadSetting("DBConnection")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SecondMinuteTariffPlan>().ToTable("TariffPlans");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Terminal>().ToTable("Terminals");
            modelBuilder.Entity<Port>().ToTable("Ports");
            modelBuilder.Entity<Contract>().ToTable("Contracts");
            modelBuilder.Entity<Station>().ToTable("Stations");
        }
    }
}
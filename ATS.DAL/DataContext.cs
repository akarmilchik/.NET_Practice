using ATS.DAL.Helpers;
using ATS.DAL.ModelsEntities;
using ATS.DAL.ModelsEntities.Billing;
using Microsoft.EntityFrameworkCore;

namespace ATS.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<SecondMinuteTariffPlanEntity> TariffPlans { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<OutgoingRequestEntity> OutgoingRequests { get; set; }
        public DbSet<RespondEntity> Responds { get; set; }
        public DbSet<CallDetailsEntity> CallsDetails { get; set; }
        public DbSet<PortEntity> Ports { get; set; }
        public DbSet<StationEntity> Stations { get; set; }
        public DbSet<TerminalEntity> Terminals { get; set; }

        public override int SaveChanges() => base.SaveChanges();

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"{ReadConfig.ReadSetting("DBConnection")}");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientEntity>().ToTable("Clients");
            modelBuilder.Entity<ContractEntity>().ToTable("Contracts");
            modelBuilder.Entity<SecondMinuteTariffPlanEntity>().ToTable("TariffPlans");
            modelBuilder.Entity<RequestEntity>().ToTable("Requests");
            modelBuilder.Entity<OutgoingRequestEntity>().ToTable("OutgoingRequests");
            modelBuilder.Entity<RespondEntity>().ToTable("Responds");
            modelBuilder.Entity<CallDetailsEntity>().ToTable("CallsDetails");
            modelBuilder.Entity<PortEntity>().ToTable("Ports");
            modelBuilder.Entity<StationEntity>().ToTable("Stations");
            modelBuilder.Entity<TerminalEntity>().ToTable("Terminals");
        }
    }
}
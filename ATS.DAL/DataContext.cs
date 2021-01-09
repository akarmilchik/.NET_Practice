using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using Microsoft.EntityFrameworkCore;

namespace ATS.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<ITariffPlan> TariffPlans { get; set; }
        public DbSet<IUser> Clients { get; set; }
        public DbSet<ITerminal> Terminals { get; set; }
        public DbSet<IPort> Ports { get; set; }
        public DbSet<IContract> Contracts { get; set; }
        public DbSet<IStation> Stations { get; set; }

        public override int SaveChanges() => base.SaveChanges();
    }
}
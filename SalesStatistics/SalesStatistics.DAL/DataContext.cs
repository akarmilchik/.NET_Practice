using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalesStatistics.DAL.Models;
using System.Threading.Tasks;

namespace SalesStatistics.DAL
{
    public class DataContext : IdentityDbContext<User>, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> operationalStoreOptions;

        public DataContext(DbContextOptions<DataContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            this.operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePersistedGrantContext(operationalStoreOptions.Value);

            modelBuilder.Entity<Models.Client>().ToTable("Clients");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(o => new { o.ClientId, o.ProductId });
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}

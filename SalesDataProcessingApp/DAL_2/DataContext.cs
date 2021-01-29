using DAL_2.ModelsEntities;
using System.Data.Entity;

namespace DAL_2
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=ProductsSalesDb")
        {
        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public override int SaveChanges() => base.SaveChanges();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>().ToTable("Orders").HasKey(e => e.Id);

            modelBuilder.Entity<OrderEntity>().HasOptional(e => e.Client);

            modelBuilder.Entity<OrderEntity>().HasOptional(e => e.Product);

            modelBuilder.Entity<ClientEntity>().ToTable("Clients").HasKey(c => c.Id);

            modelBuilder.Entity<ProductEntity>().ToTable("Products").HasKey(p => p.Id);
        }
    }
}

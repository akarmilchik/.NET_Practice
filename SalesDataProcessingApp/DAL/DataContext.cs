using DAL.ModelsEntities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=ProductsSalesDb")
        {
        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public override int SaveChanges()
        {
            bool saveFailed;

            do
            {
                saveFailed = false;

                try
                {
                    return base.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);


            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>().ToTable("Orders").HasKey(e => e.Id).HasRequired(e => e.Client);

            modelBuilder.Entity<OrderEntity>().HasRequired(e => e.Product);

            modelBuilder.Entity<ClientEntity>().ToTable("Clients").HasKey(c => c.Id);

            modelBuilder.Entity<ProductEntity>().ToTable("Products").HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

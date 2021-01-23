using DAL.ModelsEntities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public override int SaveChanges() => base.SaveChanges();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductsSalesDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.Client);

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.Product);

            modelBuilder.Entity<OrderEntity>().ToTable("Orders").HasKey(o => o.Id);

            modelBuilder.Entity<ClientEntity>().ToTable("Clients").HasKey(c => c.Id);

            modelBuilder.Entity<ProductEntity>().ToTable("Producta").HasKey(p => p.Id);
        }
    }
}

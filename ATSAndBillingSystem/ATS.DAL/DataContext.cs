﻿using ATS.DAL.ModelsEntities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientEntity>().ToTable("Clients").HasKey(e => e.Id);

            modelBuilder.Entity<ContractEntity>().ToTable("Contracts").HasKey(c => c.Id);

            modelBuilder.Entity<SecondMinuteTariffPlanEntity>().ToTable("TariffPlans").HasKey(t => t.Id);

            modelBuilder.Entity<RequestEntity>().ToTable("Requests").HasKey(r => r.Id);

            modelBuilder.Entity<OutgoingRequestEntity>().ToTable("OutgoingRequests");

            modelBuilder.Entity<RespondEntity>().ToTable("Responds").HasKey(r => r.Id);

            modelBuilder.Entity<CallDetailsEntity>().ToTable("CallDetails").HasKey(d => d.Id);

            modelBuilder.Entity<PortEntity>().ToTable("Ports").HasKey(p => p.Id);

            modelBuilder.Entity<StationEntity>().ToTable("Stations").HasKey(s => s.Id);

            modelBuilder.Entity<TerminalEntity>().ToTable("Terminals").HasKey(t => t.Id);
        }
    }
}
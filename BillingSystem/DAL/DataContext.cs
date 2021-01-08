﻿using BillingSystem.Business.Models;
using System.Data.Entity;

namespace BillingSystem.DAL
{
    class DataContext : DbContext
    {

        public DataContext() : base("DbConnection")
        {

        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<TariffPlan> TariffPlans { get; set; }

        public override int SaveChanges() => base.SaveChanges();

    }
}

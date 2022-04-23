using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Tereza.Core;

namespace Project.Tereza.Infrastructure.DBContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Need>? Needs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // here will be data to seed
            SeedNeeds(modelBuilder);
        }

        private static void SeedNeeds(ModelBuilder modelBuilder)
        {
            var needs = new List<Need>
            {
                new("82d257a5-d72b-4f08-bcf2-76ebdc958b5f", "Laptop", "Need laptop for working needs.", 1, false),
                new("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82", "Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
                new("a961067e-c777-42c2-8fee-71180d750bd7", "Aspirin", "Please, I can't find this drug in retail", 3, false)
            };

            foreach (var need in needs)
            {
                modelBuilder.Entity<Need>().HasData(need);
            }
        }
    }
}
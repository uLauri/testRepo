using DeliveryApp.Models.Fees;
using DeliveryApp.Models.Weather;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeliveryApp.Data
{
    /// <summary>
    /// ApplicationDbContext setup. 
    /// Creates 2 tables (Fees and WeatherConditions) on initial creation and populates Fees table with values given in the assignment
    /// </summary>
    public class ApplicationDbContext : DbContext
    {

        public DbSet<WeatherCondition> WeatherConditions { get; set; } 
        public DbSet<Fees> Fees { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherCondition>().HasKey(x => x.Id);
            modelBuilder.Entity<Fees>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Fees>().HasData(
                new Fees { Id = 1, City = "Tallinn", ColdATEF = 0.5, FreezingATEF = 1.0, Modified = DateTime.Today, RainEF = 0.5, RBF = 3.5, SnowEF = 1.0, Vehicle = "Scooter", WSEF = 0.0 },
                new Fees { Id = 2, City = "Tallinn", ColdATEF = 0.5, FreezingATEF = 1.0, Modified = DateTime.Today, RainEF = 0.5, RBF = 3.0, SnowEF = 1.0, Vehicle = "Bike", WSEF = 0.5 },
                new Fees { Id = 3, City = "Tallinn", ColdATEF = 0.0, FreezingATEF = 0.0, Modified = DateTime.Today, RainEF = 0.0, RBF = 4.0, SnowEF = 0.0, Vehicle = "Car", WSEF = 0.0 },
                new Fees { Id = 4, City = "Tartu", ColdATEF = 0.5, FreezingATEF = 1.0, Modified = DateTime.Today, RainEF = 0.5, RBF = 3.0, SnowEF = 1.0, Vehicle = "Scooter", WSEF = 0.0 },
                new Fees { Id = 5, City = "Tartu", ColdATEF = 0.5, FreezingATEF = 1.0, Modified = DateTime.Today, RainEF = 0.5, RBF = 2.5, SnowEF = 1.0, Vehicle = "Bike", WSEF = 0.5 },
                new Fees { Id = 6, City = "Tartu", ColdATEF = 0.0, FreezingATEF = 0.0, Modified = DateTime.Today, RainEF = 0.0, RBF = 3.5, SnowEF = 0.0, Vehicle = "Car", WSEF = 0.0 },
                new Fees { Id = 7, City = "Pärnu", ColdATEF = 0.5, FreezingATEF = 1.0, Modified = DateTime.Today, RainEF = 0.5, RBF = 2.5, SnowEF = 1.0, Vehicle = "Scooter", WSEF = 0.0 },
                new Fees { Id = 8, City = "Pärnu", ColdATEF = 0.5, FreezingATEF = 1.0, Modified = DateTime.Today, RainEF = 0.5, RBF = 2.0, SnowEF = 1.0, Vehicle = "Bike", WSEF = 0.5 },
                new Fees { Id = 9, City = "Pärnu", ColdATEF = 0.0, FreezingATEF = 0.0, Modified = DateTime.Today, RainEF = 0.0, RBF = 3.0, SnowEF = 0.0, Vehicle = "Car", WSEF = 0.0 }
                );
        }
    }
}

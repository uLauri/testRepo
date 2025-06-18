using DeliveryApp.Models.Fees;
using DeliveryApp.Models.Weather;
using Microsoft.EntityFrameworkCore;

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
                new Fees { Id = 1, City = "Tallinn", ColdAtef = 0.5, FreezingAtef = 1.0, Modified = DateTime.Today, RainEf = 0.5, Rbf = 3.5, SnowEf = 1.0, Vehicle = "Scooter", Wsef = 0.0 },
                new Fees { Id = 2, City = "Tallinn", ColdAtef = 0.5, FreezingAtef = 1.0, Modified = DateTime.Today, RainEf = 0.5, Rbf = 3.0, SnowEf = 1.0, Vehicle = "Bike", Wsef = 0.5 },
                new Fees { Id = 3, City = "Tallinn", ColdAtef = 0.0, FreezingAtef = 0.0, Modified = DateTime.Today, RainEf = 0.0, Rbf = 4.0, SnowEf = 0.0, Vehicle = "Car", Wsef = 0.0 },
                new Fees { Id = 4, City = "Tartu", ColdAtef = 0.5, FreezingAtef = 1.0, Modified = DateTime.Today, RainEf = 0.5, Rbf = 3.0, SnowEf = 1.0, Vehicle = "Scooter", Wsef = 0.0 },
                new Fees { Id = 5, City = "Tartu", ColdAtef = 0.5, FreezingAtef = 1.0, Modified = DateTime.Today, RainEf = 0.5, Rbf = 2.5, SnowEf = 1.0, Vehicle = "Bike", Wsef = 0.5 },
                new Fees { Id = 6, City = "Tartu", ColdAtef = 0.0, FreezingAtef = 0.0, Modified = DateTime.Today, RainEf = 0.0, Rbf = 3.5, SnowEf = 0.0, Vehicle = "Car", Wsef = 0.0 },
                new Fees { Id = 7, City = "Pärnu", ColdAtef = 0.5, FreezingAtef = 1.0, Modified = DateTime.Today, RainEf = 0.5, Rbf = 2.5, SnowEf = 1.0, Vehicle = "Scooter", Wsef = 0.0 },
                new Fees { Id = 8, City = "Pärnu", ColdAtef = 0.5, FreezingAtef = 1.0, Modified = DateTime.Today, RainEf = 0.5, Rbf = 2.0, SnowEf = 1.0, Vehicle = "Bike", Wsef = 0.5 },
                new Fees { Id = 9, City = "Pärnu", ColdAtef = 0.0, FreezingAtef = 0.0, Modified = DateTime.Today, RainEf = 0.0, Rbf = 3.0, SnowEf = 0.0, Vehicle = "Car", Wsef = 0.0 }
                );
        }
    }
}

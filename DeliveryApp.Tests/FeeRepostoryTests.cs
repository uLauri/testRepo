using DeliveryApp.Data.Repositories;
using DeliveryApp.Data;
using DeliveryApp.Models.Fees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Tests
{
    public class FeeRepostoryTests
    {
        [Fact]
        public async Task GetFeesByCityAndVehicleAsync_ShouldReturnCorrectFees()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Fees.Add(new Fees
                {
                    City = "Tallinn",
                    Vehicle = "car",
                    RBF = 10
                });
                await context.SaveChangesAsync();
                
                var repo = new FeeRepository(context);

                var fees = await repo.GetFeesByCityAndVehicleAsync("tallinn", "car");

                Assert.NotNull(fees);
                Assert.Equal(10, fees.RBF);
            }
        }
    }
}

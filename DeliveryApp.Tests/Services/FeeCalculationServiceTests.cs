using DeliveryApp.Data.Repositories;
using DeliveryApp.Models.Fees;
using DeliveryApp.Models.Weather;
using DeliveryApp.Services;
using Moq;

namespace DeliveryApp.Tests.Services
{
    /// <summary>
    /// Tests for main business logic calculations based on windspeed, temperature and weather phenomenas, edge cases.
    /// Structures are the same for tests, parameters vary.
    /// </summary>
    public class FeeCalculationServiceTests
    {
        [Fact]
        public async Task CalculateFee_ReturnCorrectCarFee()
        {
            var mockWeatherRepo = new Mock<IWeatherRepository>();
            var mockFeeRepo = new Mock<IFeeRepository>();

            mockWeatherRepo.Setup(repo => repo.GetWeatherConditionAsync("Tallinn"))
                .ReturnsAsync(new WeatherCondition { Name = "Tallinn", WeatherPhenomenon = "Clear", WindSpeed = 5 });

            mockFeeRepo.Setup(repo => repo.GetFeesByCityAndVehicleAsync("Tallinn", "car"))
                .ReturnsAsync(new Fees { Rbf = 10 });

            var service = new FeeCalculationService(mockWeatherRepo.Object, mockFeeRepo.Object);

            var result = await service.CalculateFee("Tallinn", "car");

            Assert.Equal(10, result);
        }

        [Fact]
        public async Task CalculateFee_ReturnZero_WhenBikeWindSpeedTooHigh()
        {
            var mockWeatherRepo = new Mock<IWeatherRepository>();
            var mockFeeRepo = new Mock<IFeeRepository>();

            mockWeatherRepo.Setup(repo => repo.GetWeatherConditionAsync("tallinn"))
                .ReturnsAsync(new WeatherCondition { Name = "tallinn", WeatherPhenomenon = "Clear", WindSpeed = 25 });

            mockFeeRepo.Setup(repo => repo.GetFeesByCityAndVehicleAsync("tallinn", "bike"))
                .ReturnsAsync(new Fees { Rbf = 5 });

            var service = new FeeCalculationService(mockWeatherRepo.Object, mockFeeRepo.Object);

            var result = await service.CalculateFee("tallinn", "bike");

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task CalculateFee_IncludeFreezingWeatherExtraFee_ForScooterOrBike()
        {
            var mockWeatherRepo = new Mock<IWeatherRepository>();
            var mockFeeRepo = new Mock<IFeeRepository>();

            mockWeatherRepo.Setup(repo => repo.GetWeatherConditionAsync("tartu"))
                .ReturnsAsync(new WeatherCondition { Name = "Tartu", WeatherPhenomenon = "Clear", AirTemperature = -11 });

            mockFeeRepo.Setup(repo => repo.GetFeesByCityAndVehicleAsync("tartu", "bike"))
                .ReturnsAsync(new Fees { Rbf = 5, FreezingAtef = 2 });

            var service = new FeeCalculationService(mockWeatherRepo.Object, mockFeeRepo.Object);

            var result = await service.CalculateFee("tartu", "bike");

            Assert.Equal(7, result);
        }

        [Fact]
        public async Task CalculateFee_IncludeColdWeatherExtraFee_ForScooterOrBike()
        {
            var mockWeatherRepo = new Mock<IWeatherRepository>();
            var mockFeeRepo = new Mock<IFeeRepository>();

            mockWeatherRepo.Setup(repo => repo.GetWeatherConditionAsync("tartu"))
                .ReturnsAsync(new WeatherCondition { Name = "Tartu", WeatherPhenomenon = "Clear", AirTemperature = -5 });

            mockFeeRepo.Setup(repo => repo.GetFeesByCityAndVehicleAsync("tartu", "scooter"))
                .ReturnsAsync(new Fees { Rbf = 5, ColdAtef = 1 });

            var service = new FeeCalculationService(mockWeatherRepo.Object, mockFeeRepo.Object);

            var result = await service.CalculateFee("tartu", "scooter");

            Assert.Equal(6, result);
        }
        [Fact]
        public async Task CalculateFee_IncludeRainyAndWindyWeatherExtraFee_ForBike()
        {
            var mockWeatherRepo = new Mock<IWeatherRepository>();
            var mockFeeRepo = new Mock<IFeeRepository>();

            mockWeatherRepo.Setup(repo => repo.GetWeatherConditionAsync("tartu"))
                .ReturnsAsync(new WeatherCondition { Name = "Tartu", WeatherPhenomenon = "Heavy rain", AirTemperature = 10, WindSpeed = 15 });

            mockFeeRepo.Setup(repo => repo.GetFeesByCityAndVehicleAsync("tartu", "bike"))
                .ReturnsAsync(new Fees { Rbf = 5, RainEf = 5, Wsef = 3 });

            var service = new FeeCalculationService(mockWeatherRepo.Object, mockFeeRepo.Object);

            var result = await service.CalculateFee("tartu", "bike");

            Assert.Equal(13, result);
        }

        [Fact]
        public async Task CalculateFee_IncludeSnowyAndWindyWeatherExtraFee_ForBike()
        {
            var mockWeatherRepo = new Mock<IWeatherRepository>();
            var mockFeeRepo = new Mock<IFeeRepository>();

            mockWeatherRepo.Setup(repo => repo.GetWeatherConditionAsync("tartu"))
                .ReturnsAsync(new WeatherCondition { Name = "Tartu", WeatherPhenomenon = "Moderate snowfall", AirTemperature = 10, WindSpeed = 12 });

            mockFeeRepo.Setup(repo => repo.GetFeesByCityAndVehicleAsync("tartu", "bike"))
                .ReturnsAsync(new Fees { Rbf = 5, SnowEf = 7, Wsef = 2 });

            var service = new FeeCalculationService(mockWeatherRepo.Object, mockFeeRepo.Object);

            var result = await service.CalculateFee("tartu", "bike");

            Assert.Equal(14, result);
        }

        [Fact]
        public async Task CalculateFee_ExtremeConditionsShouldReturnZero_ForBike()
        {
            var mockWeatherRepo = new Mock<IWeatherRepository>();
            var mockFeeRepo = new Mock<IFeeRepository>();

            mockWeatherRepo.Setup(repo => repo.GetWeatherConditionAsync("tartu"))
                .ReturnsAsync(new WeatherCondition { Name = "Tartu", WeatherPhenomenon = "Hail", AirTemperature = 10, WindSpeed = 12 });

            mockFeeRepo.Setup(repo => repo.GetFeesByCityAndVehicleAsync("tartu", "bike"))
                .ReturnsAsync(new Fees { Rbf = 5, SnowEf = 7, Wsef = 2 });

            var service = new FeeCalculationService(mockWeatherRepo.Object, mockFeeRepo.Object);

            var result = await service.CalculateFee("tartu", "bike");

            Assert.Equal(0, result);
        }
    }
}
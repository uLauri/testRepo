using DeliveryApp.Data.Repositories;
using DeliveryApp.Models.Weather;
using DeliveryApp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task RetrieveWeatherData_ShouldParseAndSaveWeatherConditions()
        {
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            var mockXmlService = new Mock<IXmlProcessingService>();
            var mockWeatherRepo = new Mock<IWeatherRepository>();

            var fakeXmlResponse = "<Observations timestamp=1625097600 ><Stations>"
                                + "<Station><Name>Tallinn-Harku</Name><WmoCode>123</WmoCode><AirTemperature>10</AirTemperature><WindSpeed>5</WindSpeed><Phenomenon>Clear</Phenomenon></Station>"
                                + "<Station><Name>Unknown</Name><WmoCode>999</WmoCode><AirTemperature>15</AirTemperature><WindSpeed>7</WindSpeed><Phenomenon>Cloudy</Phenomenon></Station>"
                                + "</Stations></Observations>";

            var httpClient = new HttpClient(new FakeHttpMessageHandler(fakeXmlResponse));

            mockXmlService.Setup(xml => xml.DeserializeFromXml(fakeXmlResponse))
                          .Returns(new Observations
                          {
                              Stations = new List<Station>
                              {
                              new Station { Name = "Tallinn-Harku", WmoCode = 123, AirTemperature = 10, WindSpeed = 5, Phenomenon = "Clear" },
                              new Station { Name = "Unknown", WmoCode = 999, AirTemperature = 15, WindSpeed = 7, Phenomenon = "Cloudy" }
                              },
                              Timestamp = "1625097600"
                          });

            var service = new WeatherService(new HttpClientFactoryStub(httpClient), mockXmlService.Object, mockWeatherRepo.Object);

            await service.RetrieveAndSaveWeatherData("https://fake-api.com/weather");

            mockWeatherRepo.Verify(repo => repo.SaveWeatherConditionsAsync(It.Is<List<WeatherCondition>>(conditions =>
                conditions.Any(c => c.Name == "Tallinn-Harku" && c.WmoCode == 123)
            )), Times.Once);
        }
    }
}

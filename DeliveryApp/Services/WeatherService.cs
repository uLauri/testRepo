using DeliveryApp.Data.Repositories;
using DeliveryApp.Models.Weather;

namespace DeliveryApp.Services
{
    /// <summary>
    /// Main BL to execute Cronjob and retrieve weather data from Ilmateenistus.
    /// </summary>
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IXmlProcessingService _xmlProcessingService;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService (IHttpClientFactory httpClientFactory, IXmlProcessingService xmlProcessingService, IWeatherRepository weatherRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _xmlProcessingService = xmlProcessingService;
            _weatherRepository = weatherRepository;
        }   
        /// <summary>
        /// Main method of retireving and saving weather data from Ilmateenistus, uses main api address as configured in the application.json
        /// </summary>
        /// <param name="weatherServiceApi"></param>
        /// <returns></returns>
        public async Task RetrieveAndSaveWeatherData(string weatherServiceApi)
        {
            var response = await _httpClient.GetStringAsync(weatherServiceApi);
            var observations = _xmlProcessingService.DeserializeFromXml(response);
            if (observations != null)
            {
                var stations = observations.Stations
                    .Where(station => station.Name == "Tallinn-Harku" ||
                                      station.Name == "Tartu-Tõravere" ||
                                      station.Name == "Pärnu")
                    .ToList();

                await SaveWeatherDataAsync(stations, observations.Timestamp);
            }
        }
        /// <summary>
        /// Saving Tallinn, Pärnu and Tartu stations weather data to db.
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private async Task SaveWeatherDataAsync(List<Station> stations, string timestamp)
        {
#pragma warning disable CS8629 // Nullable value type may be null. Supposed to crash when value is null, to avoid saving bad data.
#pragma warning disable CS8601 // -||-
            var weatherConditions = stations.Select(station => new WeatherCondition
            {
                Name = station.Name,
                WmoCode = station.WmoCode,
                AirTemperature = (double)station.AirTemperature,
                WindSpeed = (double)station.WindSpeed,
                WeatherPhenomenon = string.IsNullOrEmpty(station.Phenomenon) ? string.Empty : station.Phenomenon,
                TimeStamp = long.Parse(timestamp),
                CreatedAt = DateTime.Now
            }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8629 // Nullable value type may be null.

            await _weatherRepository.SaveWeatherConditionsAsync(weatherConditions);            
        }
    }
}

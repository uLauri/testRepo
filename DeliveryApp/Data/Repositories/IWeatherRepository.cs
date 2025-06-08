using DeliveryApp.Models.Weather;

namespace DeliveryApp.Data.Repositories
{
    public interface IWeatherRepository
    {
        Task<WeatherCondition> GetWeatherConditionAsync(string city);
        Task SaveWeatherConditionsAsync(List<WeatherCondition> conditions);
    }
}

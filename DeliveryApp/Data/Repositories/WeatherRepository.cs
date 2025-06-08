using DeliveryApp.Models.Weather;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Data.Repositories
{
    /// <summary>
    /// Data-access layer to retrieve and save weather data
    /// </summary>
    public class WeatherRepository : IWeatherRepository 
    {
        private readonly ApplicationDbContext _context;

        public WeatherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method to retrieve given city latest weather conditions, based on city and timestamp.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<WeatherCondition> GetWeatherConditionAsync(string city)
        {
            return await _context.WeatherConditions
                .Where(conditions => conditions.Name.ToLower().Contains(city))
                .OrderByDescending(conditions => conditions.TimeStamp)
                .FirstAsync(); 
        }
        /// <summary>
        /// Save weather conditions to DB which are requested from Ilmateenistus.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task SaveWeatherConditionsAsync(List<WeatherCondition> conditions)
        {
            _context.WeatherConditions.AddRange(conditions);
            await _context.SaveChangesAsync();
        }
    }
}

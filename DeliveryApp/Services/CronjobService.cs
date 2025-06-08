using Cronos;
using DeliveryApp.Data;
using DeliveryApp.Models.Weather;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace DeliveryApp.Services 
{ 
    /// <summary>
    /// Main cronjob service to retrieve and save weather data. Main schedule and weather API endpoint are held in config, 
    /// added fallbacks in case missing from config. Runs every hour +15 min by default. 
    /// To modify schedule, change the main schedule in the application.json file. 
    /// </summary>
    public class CronjobService : BackgroundService
    {
        private readonly string _cronExpression;
        private readonly string _weatherServiceApi;
        private DateTime _nextRun;
        private readonly IServiceScopeFactory _scopeFactory;   

        public CronjobService (IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _cronExpression = configuration["CronSchedule"] ?? "15 * * * *";
            _weatherServiceApi = configuration["WeatherServiceApi"] ?? "https://www.ilmateenistus.ee/ilma_andmed/xml/observations.php";
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cronSchedule = CronExpression.Parse(_cronExpression);
                    _nextRun = cronSchedule.GetNextOccurrence(DateTime.UtcNow) ?? DateTime.UtcNow.AddMinutes(60);
                    var delay = (_nextRun - DateTime.UtcNow).TotalMilliseconds;

                    using(var scope = _scopeFactory.CreateScope())
                    {
                        var weatherService = scope.ServiceProvider.GetRequiredService<WeatherService>();
                        await weatherService.RetrieveAndSaveWeatherData(_weatherServiceApi);
                        Console.WriteLine($"Retrieved weather data from Ilmateenistus at  {DateTime.Now}");
                    }

                    await Task.Delay((int)delay, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in scheduled retrieval of weather data: {ex.Message}");
                    await Task.Delay(10000, stoppingToken); //in case of an exception, wait not to bombard Ilmateenistuse API
                }
            }
        }
    }
}

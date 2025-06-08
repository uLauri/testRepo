using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Models.Weather
{
    /// <summary>
    /// Weather conditions model saved to the db and used for main delivery fee calculation.
    /// </summary>
    public class WeatherCondition
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public long? WmoCode { get; set; }
        public double AirTemperature { get; set; }
        public double WindSpeed { get; set; }
        public string WeatherPhenomenon { get; set; } = string.Empty;
        public long TimeStamp { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

using DeliveryApp.Models.Fees;
using DeliveryApp.Models.Weather;

namespace DeliveryApp.Services
{
    public interface IFeeCalculationService
    {
        Task<double> CalculateFee(string city, string vehicle);
        double CalculateScooterAndBikeFees(Fees fees, WeatherCondition conditions);
    }
}

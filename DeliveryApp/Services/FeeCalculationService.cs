using DeliveryApp.Data.Repositories;
using DeliveryApp.Models;
using DeliveryApp.Models.Fees;
using DeliveryApp.Models.Weather;
using System.Text.RegularExpressions;

namespace DeliveryApp.Services
{
    /// <summary>
    /// BL to calculate delivery fee.
    /// </summary>
    public class FeeCalculationService : IFeeCalculationService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IFeeRepository _feeRepository;
        public FeeCalculationService(IWeatherRepository weatherRepository, IFeeRepository feeRepository) 
        {
            _weatherRepository = weatherRepository;
            _feeRepository = feeRepository;
        }
        /// <summary>
        /// Main method for calculating delivery fee. Retrieves the latest weatherconditions, vehicle and city based feesfrom db and calculates delivery fee.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        public async Task<double> CalculateFee(string city, string vehicleType)
        {
            double finalFee = 0.0;
            var conditions = await _weatherRepository.GetWeatherConditionAsync(city);

            var pattern = "(hail|thunder|glaze)";
            if (Regex.IsMatch(conditions.WeatherPhenomenon, pattern, RegexOptions.IgnoreCase) ||
                ((conditions.WindSpeed > 20) && vehicleType == "bike"))
                return finalFee;

            var fees = await _feeRepository.GetFeesByCityAndVehicleAsync(city, vehicleType);

            if (vehicleType == "car")
            {
                return fees.Rbf;
            }
            else
            {
                finalFee = CalculateScooterAndBikeFees(fees, conditions);

                if (vehicleType == "bike" && (conditions.WindSpeed >= 10) && (conditions.WindSpeed <= 20))
                {
                    finalFee += fees.Wsef;
                }
            }

            return finalFee;
        }

        /// <summary>
        /// Calculates extra fees and edge cases in case the given vehicle types are scooter or bike.
        /// </summary>
        /// <param name="fees"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public double CalculateScooterAndBikeFees(Fees fees, WeatherCondition conditions)
        {
            var finalFee = fees.Rbf;

            if (conditions.AirTemperature < -10)
                finalFee += fees.FreezingAtef;

            if (-10 <= conditions.AirTemperature && conditions.AirTemperature <= 0)
                finalFee += fees.ColdAtef;

            var snowConditions = Rules.SnowConditions;
            var rainyConditions = Rules.RainyConditions;

            if (snowConditions.Contains(conditions.WeatherPhenomenon.ToLower()))
                finalFee += fees.SnowEf;

            if (rainyConditions.Contains(conditions.WeatherPhenomenon.ToLower()))
                finalFee += fees.RainEf;
            
            return finalFee;
        }
    }
}

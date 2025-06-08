using DeliveryApp.Models.Fees;

namespace DeliveryApp.Data.Repositories
{
    public interface IFeeRepository
    {
        Task<Fees> GetFeesByCityAndVehicleAsync(string city, string vehicle);
        Task UpdateFeesByCityAndVehicleAsync(Fees fees, string vehicle);
    }
}

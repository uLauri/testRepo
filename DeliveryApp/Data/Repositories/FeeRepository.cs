using DeliveryApp.Models.Fees;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Data.Repositories
{
    /// <summary>
    /// Data access layer to retrieve Fees based on city and vehicle and Update fees (not implemented)
    /// </summary>
    public class FeeRepository : IFeeRepository
    {
        private readonly ApplicationDbContext _context;

        public FeeRepository (ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves Fees based on given city and vehicle from db.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public async Task<Fees> GetFeesByCityAndVehicleAsync(string city, string vehicle)
        {
            return await _context.Fees.Where(f => f.City.ToLower() == city && f.Vehicle.ToLower() == vehicle).FirstAsync();
        }
        /// <summary>
        /// Meant to update based on a POST request JSON data (FeeUpdateRequest), but not in use.
        /// </summary>
        /// <param name="fees"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public async Task UpdateFeesByCityAndVehicleAsync(Fees fees, string vehicle)
        {
            var currentFees = await _context.Fees.Where(f => f.City == fees.City && f.Vehicle == vehicle).FirstAsync();
            if (currentFees != null)
            {
                currentFees.Wsef = fees.Wsef;
                currentFees.FreezingAtef = fees.FreezingAtef;
                currentFees.Rbf = fees.Rbf;
                currentFees.SnowEf = fees.SnowEf;
                currentFees.RainEf = fees.RainEf;
                currentFees.ColdAtef = fees.ColdAtef;
                currentFees.Modified = DateTime.Now;
                
                await _context.SaveChangesAsync();
            }
        }
    }
}

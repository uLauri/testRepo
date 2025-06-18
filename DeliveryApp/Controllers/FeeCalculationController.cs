using DeliveryApp.Models;
using DeliveryApp.Models.Fees;
using DeliveryApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    /// <summary>
    /// Main controller with REST endpoint for asking a delivery fee based on given town and vehicle type
    /// Method-GET
    /// Query Parameters are city and vehicle
    /// Returns delivery fee in FeeResponse format (given city, vehicle and calculated fee)
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/delivery")]
    public class FeeCalculationController : ControllerBase
    {
        private readonly IFeeCalculationService _feeCalculationService;

        public FeeCalculationController(IFeeCalculationService feeCalculationService)
        {
            _feeCalculationService = feeCalculationService;
        }

        [HttpGet("calculate-fee")]
        public async Task<IActionResult> CalculateFee([FromQuery] string city, [FromQuery] string vehicle) 
        {
            if (string.IsNullOrEmpty(city) || 
                string.IsNullOrEmpty(vehicle) ||
                !Rules.Vehicles.Contains(vehicle.ToLower()) ||
                !Rules.Cities.Contains(city.ToLower()))
                return BadRequest("Invalid city or vehicle parameters!");
            
            var fee = await _feeCalculationService.CalculateFee(city.ToLower(), vehicle.ToLower());
            
            if (fee == 0.0)
                return Ok(new { ErrorMessage = "Usage of selected vehicle type is forbidden!" });
            
            return Ok (new FeeResponse(city, vehicle, fee));
        }


        //not implemented yet

        //[HttpPost("update-fee")]
        //public async Task<IActionResult> UpdateFee([FromBody] FeeUpdateRequest request)
        //{
        //    if (string.IsNullOrEmpty(request.City) || string.IsNullOrEmpty(request.Vehicle))
        //        return BadRequest("Missing city or vehicle type in the update request!");

        //    await _feeCalculationService.UpdateFeeAsync(request);
        //    return Ok(new { Message = "Fee updated successfully! ", request });
        //}

    }
}

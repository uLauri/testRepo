namespace DeliveryApp.Models.Fees
{/// <summary>
/// Response model for the REST API endpoint
/// </summary>
    public class FeeResponse
    {
        /// <summary>
        /// City given as an input parameter
        /// </summary>
        public string City { get; set; } 
        /// <summary>
        /// Vehicle type given as an input parameter
        /// </summary>
        public string Vehicle { get; set; } 
        /// <summary>
        /// Returnable calculated fee for given transport, weather conditions and city
        /// </summary>
        public double DeliveryFee { get; set; }

        public FeeResponse(string city, string vehicle, double deliveryFee)
        {
            City = city;
            Vehicle = vehicle;
            DeliveryFee = deliveryFee;
        }
    }
}

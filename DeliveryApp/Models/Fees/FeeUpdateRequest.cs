namespace DeliveryApp.Models.Fees
{
    /// <summary>
    /// Not implemented, but meant to use as a DTO to update fees in the DB.
    /// </summary>
    public class FeeUpdateRequest
    {
        public string City { get; set; } = string.Empty;
        public string Vehicle { get; set; } = string.Empty;
        public double? RBF { get; set; }
        public double? FreezingATEF { get; set; }
        public double? ColdATEF { get; set; }
        public double? WSEF { get; set; }
        public double? SnowEF { get; set; }
        public double? RainEF { get; set; }
    }
}

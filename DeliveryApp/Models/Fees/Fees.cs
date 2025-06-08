using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Models.Fees
{
    /// <summary>
    /// Supporting table created and populated on database initialization with given data in the assignment. Used to help calculate delivery fees in main BL.
    /// </summary>
    public class Fees
    {
        [Key]
        public int Id { get; set; }
        //Tallinn, Tartu or Pärnu
        public string City { get; set; } = string.Empty;
        //Bike, car or scooter
        public string Vehicle { get; set; } = string.Empty;
        //Base fee for given transportation in given city
        public double RBF { get; set; }
        //Extra fee if temp is below -10c
        public double FreezingATEF { get; set; }
        //Extra fee if temp is between -10c and 0c
        public double ColdATEF { get; set; }
        //Extra fee for bikes in case strong wind
        public double WSEF { get; set; }
        //Extra fee in case snowy weather
        public double SnowEF { get; set; }
        //Extra fee in case rainy weather
        public double RainEF { get; set; }
        public DateTime Modified { get; set; }
    }
}

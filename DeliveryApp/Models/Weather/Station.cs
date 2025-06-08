using System.ComponentModel;
using System.Xml.Serialization;

namespace DeliveryApp.Models.Weather
{
    /// <summary>
    /// XML model used to manipulate data retrieved from Ilmateenistus.
    /// </summary>
    [XmlRoot("station")]
    public class Station
    {
        [XmlElement("name")]
        public string? Name { get; set; }

        [XmlElement("wmocode")]
        public long? WmoCode { get; set; }

        [XmlElement("phenomenon")]
        public string? Phenomenon { get; set; }

        [XmlElement("airtemperature")]
        public double? AirTemperature { get; set; }

        [XmlElement("windspeed")]
        public double? WindSpeed { get; set; }

    }
}

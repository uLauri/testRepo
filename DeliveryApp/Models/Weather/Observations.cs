using System.Xml.Serialization;

namespace DeliveryApp.Models.Weather
{
/// <summary>
/// XML-structure to read data from Ilmateenistus
/// </summary>
    [XmlRoot("observations")]
    public class Observations
    {
        [XmlAttribute("timestamp")]
        public required string Timestamp { get; set; }
        [XmlElement("station")]
        public List<Station> Stations { get; set; } = new();
    }
}

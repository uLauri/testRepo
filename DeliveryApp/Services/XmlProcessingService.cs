using DeliveryApp.Models.Weather;
using System.Xml.Serialization;

namespace DeliveryApp.Services
{
    public class XmlProcessingService : IXmlProcessingService
    {
        /// <summary>
        /// Method to serialize given xml and map it to Observations format. 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Observations? DeserializeFromXml(string xml)
        {
            xml = RemoveMissingFields(xml);
            var xmlSerializer = new XmlSerializer(typeof(Observations));
            using var stringReader = new StringReader(xml);
            
            return xmlSerializer.Deserialize(stringReader) as Observations ?? new Observations { Timestamp = "0", Stations = new List<Station>() };
        }
        /// <summary>
        /// Method to remove missing fields(from the ones we only read) from stations which don't have them populated to Deserialize successfully.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string RemoveMissingFields(string xml)
        {
            var processedXml = xml.Replace("<wmocode></wmocode>", "")
                                  .Replace("<windspeed></windspeed>", "")
                                  .Replace("<phenomenon></phenomenon>", "")
                                  .Replace("<airtemperature></airtemperature>", "");
            return processedXml;
        }
    }
}

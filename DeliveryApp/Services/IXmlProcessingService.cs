using DeliveryApp.Models.Weather;

namespace DeliveryApp.Services
{
    public interface IXmlProcessingService
    {
        Observations? DeserializeFromXml(string xml);
    }
}

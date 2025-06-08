namespace DeliveryApp.Models
{
    /// <summary>
    /// Helper arrays to differentiate rainy and snowy conditions in weather phenomenons and to exclude unsupported vehicles or cities at the main endpoint.
    /// </summary>
    public static class Rules
    {
        public static readonly string[] SnowConditions = 
        {
            "light snow shower",
            "heavy snow shower",
            "moderate snow shower",
            "light sleet",
            "moderate sleet",
            "light snowfall",
            "moderate snowfall",
            "heavy snowfall"};

        public static readonly string[] RainyConditions = 
        {
            "light shower",
            "moderate shower",
            "heavy shower",
            "light rain",
            "moderate rain",
            "heavy rain"
        };

        public static readonly string[] Vehicles =
        {
            "car",
            "bike",
            "scooter"
        };
        
        public static readonly string[] Cities =
{
            "tallinn",
            "tartu",
            "pärnu"
        };
    }
}

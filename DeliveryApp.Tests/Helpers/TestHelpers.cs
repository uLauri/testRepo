using Microsoft.Extensions.Configuration;

namespace DeliveryApp.Tests.Helpers
{
    public static class TestHelpers
    {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        public static IConfiguration CreateTestConfig() => new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                { "Jwt:Secret", "testjwtsecret123456789_qwertyasdf" },
                { "Jwt:Issuer", "test-issuer" }
                })
                .Build();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
    }
}




namespace DeliveryApp.Tests.Helpers
{
    public class HttpClientFactoryStub : IHttpClientFactory
    {
        private readonly HttpClient _client;

        public HttpClientFactoryStub(HttpClient client)
        {
            _client = client;
        }

        public HttpClient CreateClient(string name) => _client;
    }
}

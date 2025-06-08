using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Tests
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

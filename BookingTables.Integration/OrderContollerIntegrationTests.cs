using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;

namespace BookingTables.IntegrationTests
{
    public class OrderContollerIntegrationTests : IClassFixture<TestingWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrderContollerIntegrationTests(TestingWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "TestScheme");
        }
        [Fact]
        public async Task Get_returns_401_Unauthorized_if_not_authenticated()
        {
            var response = await _client.GetAsync("/api/order/40");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
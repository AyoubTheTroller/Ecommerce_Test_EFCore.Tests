using System.Text;
using System.Text.Json;
using Ecommerce.Models;

namespace EF_Core_Project_Ecommerce.Tests
{
    public class OrderIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;

        public OrderIntegrationTests()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5079") };
        }

        [Fact]
        public async Task GetOrders_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/orders");
            response.EnsureSuccessStatusCode();
        }

        //[Fact]
        public async Task CreateOrder_ReturnsSuccessStatusCode()
        {
            var orderDetail1 = new OrderDetail 
            {
                productId = 5,
            };

            var orderDetail2 = new OrderDetail 
            {
                productId = 5,
            };

            var newOrder = new Order 
            {
                userId = 1,
                dateTime = DateTime.Now,
                OrderDetails = new List<OrderDetail> { orderDetail1, orderDetail2 }
            };

            var orderJson = JsonSerializer.Serialize(newOrder);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/orders/create", orderContent);
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

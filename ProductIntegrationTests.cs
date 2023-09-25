using System.Text;
using System.Text.Json;
using Ecommerce.Models;

namespace EF_Core_Project_Ecommerce.Tests
{
    public class ProductIntegrationsTests : IDisposable
    {
        private readonly HttpClient _client;

        public ProductIntegrationsTests()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5079") };
        }

        [Fact]
        public async Task GetProducts_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/products");
            response.EnsureSuccessStatusCode();
        }

        //[Fact]
        public async Task CreateProduct_ReturnsSuccessStatusCode()
        {

            var newProduct = new Product { name = "Sony Xperia X4", categoryId = 0, price = 699};
            var productJsonm = JsonSerializer.Serialize(newProduct);
            var productContent = new StringContent(productJsonm, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/products/create", productContent);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductsByCategorySlug_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/products/category/electronics");
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

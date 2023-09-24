using System.Text;
using System.Text.Json;
using Ecommerce.Models;

namespace EF_Core_Project_Ecommerce.Tests
{
    public class CategoryIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;

        public CategoryIntegrationTests()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5079") };
        }

        [Fact]
        public async Task GetCategory_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/categories");
            response.EnsureSuccessStatusCode();
        }

        //[Fact]
        public async Task CreateCategory_ReturnsSuccessStatusCode()
        {

            var newCategory = new Category{ name = "Libri", description = "comprende tutti i libri e collezioni" };
            var categoryJson = JsonSerializer.Serialize(newCategory);
            var categoryContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/categories/create", categoryContent);
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

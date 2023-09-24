using System.Text;
using System.Text.Json;
using Ecommerce.Models;

namespace EF_Core_Project_Ecommerce.Tests
{
    public class UserIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;

        public UserIntegrationTests()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5079") };
        }

        [Fact]
        public async Task GetUsers_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/users");
            response.EnsureSuccessStatusCode();
        }

        //[Fact]
        public async Task CreateUser_ReturnsSuccessStatusCode()
        {
            var newUser = new User { Name = "Pinco", Surname = "Pallino", Username = "PINCOOOO", Password = "NOTLIKETHIS" };
            var userJson = JsonSerializer.Serialize(newUser);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/users/create", userContent);
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

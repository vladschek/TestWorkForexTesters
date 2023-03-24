using Amazon.Util;
using Common.DTOs;
using Core.Domain.Entities;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Tests
{
    public class UserTests
    {
        private readonly HttpClient _client;

        public UserTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000") 
            };
        }
        [Fact]
        public async Task GetUserById()
        {
            // Arrange
            int userId = 1;

            // Act
            var response = await _client.GetAsync($"/api/user/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            ReadUserDTO user = null;
            try
            {
                user = JsonSerializer.Deserialize<ReadUserDTO>(content);
                Assert.Equal(userId, user.Id);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                Assert.True(false, "Failed to deserialize JSON response");
            }

            Assert.Equal(user.Projects[0].UserId, user.Id);
            Assert.Equal(user.UserSettings.UserId, user.Id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetUsers()
        {
            var response = await _client.GetAsync($"/api/user");
            var content = await response.Content.ReadAsStringAsync();

            var users = JsonSerializer.Deserialize<List<ReadUserDTO>>(content);

            Assert.NotEqual(users.Count, 0);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task PostUser()
        {
            var user = new UserCreateDTO
            {
                Name = "Test Name",
                Email = "test@post.com"
            };
            var response = await _client.PostAsync($"/api/user", new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();

            int userId = JsonSerializer.Deserialize<int>(content);

            response = await _client.GetAsync($"/api/user/{userId}");

            content = await response.Content.ReadAsStringAsync();

            var userResponse = JsonSerializer.Deserialize<ReadUserDTO>(content);
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
            Assert.Equal(user.Name, userResponse.Name);
            Assert.Equal(user.Email, userResponse.Email);
        }
        [Fact]
        public async Task UpdateUser()
        {
            int userId = 2;
            var user = new UserCreateDTO
            {
                Name = "Bill Bob",
                Email = "billibobston@post.com"
            };
            var response = await _client.PutAsync($"/api/user/{userId}", new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

            Assert.Equal(response.StatusCode, HttpStatusCode.NoContent);

            response = await _client.GetAsync($"/api/user/{userId}");

            var content = await response.Content.ReadAsStringAsync();

            var userResponse = JsonSerializer.Deserialize<ReadUserDTO>(content);

            Assert.Equal(user.Name, userResponse.Name);
            Assert.Equal(user.Email, userResponse.Email);
        }
        [Fact]
        public async Task UserExist()
        {
            var response = await _client.GetAsync($"/api/user/1/exist");
            var notFoundResponse = await _client.GetAsync($"/api/user/9999/exist");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, notFoundResponse.StatusCode);
        }
        [Fact]
        public async Task DeleteUser()
        {
            var response = await _client.DeleteAsync($"/api/user/3");

            var findUserResponse = await _client.GetAsync($"/api/user/3");

            Assert.Equal(HttpStatusCode.NotFound, findUserResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

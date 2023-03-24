using AutoMapper;
using Common.DTOs;
using Core.Domain.Entities;
using Core.Interfaces.Repositories.Postgre;
using Infrustructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class UserServiceTets
    {
        public class TestHttpMessageHandler : HttpMessageHandler
        {
            private readonly Dictionary<string, HttpResponseMessage> _responses;

            public TestHttpMessageHandler(Dictionary<string, HttpResponseMessage> responses)
            {
                _responses = responses;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (_responses.TryGetValue(request.RequestUri.AbsolutePath, out HttpResponseMessage response))
                {
                    return Task.FromResult(response);
                }
                else
                {
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
                }
            }
        }
        [Fact]
        public async Task GetUserById_ReturnsCorrectUser()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            
            HttpResponseMessage userSettingsResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage projectResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[\r\n  {\r\n    \"userId\": 1,\r\n    \"name\": \"My Project\",\r\n    \"charts\": []\r\n  }\r\n]\r\n",
                Encoding.UTF8, "application/json")
            };

            
            var testHttpMessageHandler = new TestHttpMessageHandler(new Dictionary<string, HttpResponseMessage>
            {
                { "/api/usersettings/1", userSettingsResponse },
                { "/api/projects/1", projectResponse }
            });
            var httpClient = new HttpClient(testHttpMessageHandler)
            {
                BaseAddress = new Uri("http://localhost:5001")
            };

            
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);
            
            // Set up your other mocks
            var testUser = new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                SubscriptionId = 1
            };
            var expectedResult = new ReadUserDTO
            {
                Id = testUser.Id,
                Name = testUser.Name,
                Email = testUser.Email,
                SubscriptionId = testUser.SubscriptionId
            };
            mockMapper.Setup(mapper => mapper.Map<ReadUserDTO>(It.Is<User>(u => u == testUser))).Returns(expectedResult);
            mockUserRepository.Setup(repo => repo.GetById(It.Is<int>(id => id == 1), false)).ReturnsAsync(testUser);

            // Act
            var userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockHttpClientFactory.Object);
            var result = await userService.GetUserById(1);

            // Assert
            Assert.Equal(testUser.Id, result.Id);
            Assert.Equal(testUser.Name, result.Name);
            Assert.Equal(testUser.Email, result.Email);
            Assert.Equal(testUser.SubscriptionId, result.SubscriptionId);
        }
    }
}

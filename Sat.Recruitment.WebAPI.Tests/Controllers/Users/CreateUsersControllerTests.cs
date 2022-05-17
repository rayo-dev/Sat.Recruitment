using Newtonsoft.Json;
using Sat.Recruitment.Api;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.WebAPI.Tests.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace Sat.Recruitment.WebAPI.Tests.Controllers.Users
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CreateUsersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CustomWebApplicationFactory<Startup> _factory;
        readonly HttpClient _client;
        public CreateUsersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GivenCreateUserCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateUserCommand
            {
                Name = "Juan Perez " + new Random().Next(1, 100),
                Address = "Av. Arequipa 2331 Lince",
                Email = "juan@sonic.nz",
                Money = 122.0m,
                Phone = "+610221221",
                UserType = "Normal"
            };

            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/users/create", content);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenExistingUserCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateUserCommand
            {
                Name = "Juan",
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Phone = "+5491154762312",
                UserType = "Normal"
            };

            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/users/create", content);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GivenWrongEmailCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateUserCommand
            {
                Name = "Juan",
                Address = "Peru 2464",
                Email = "Juan@marmol",
                Money = 1234,
                Phone = "+5491154762312",
                UserType = "Normal"
            };

            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/users/create", content);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GivenUserWithoutUserTypeCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateUserCommand
            {
                Name = "Juan",
                Address = "Peru 2464",
                Email = "Juan@marmol",
                Money = 1234,
                Phone = "+5491154762312",
                UserType = string.Empty
            };

            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/users/create", content);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Application.Common.Response;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.WebAPI.Tests.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace Sat.Recruitment.WebAPI.Tests.Controllers.Users
{
    [CollectionDefinition("Tests")]
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
                Name = "Juan Ferrari " + new Random().Next(5, 5000),
                Address = "Av. Arequipa 2331 Lince",
                Email = "juan_fer@zoni.pe",
                Money = 12.50m,
                Phone = "+610221221321",
                UserType = "Normal"
            };

            var result = await Helpers.PostAsync<Result>(_client, "/api/users/create", command);
            Assert.Equal(Result.Success().Succeeded, result.Succeeded);
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

            var result = await Helpers.PostAsync<Result>(_client, "/api/users/create", command);
            Assert.Contains("Business exception \"User already exists\".", result.Errors);
        }

        [Fact]
        public async Task GivenWrongEmailCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateUserCommand
            {
                Name = "Luis",
                Address = "Santiago 20 Ovalo San Juan",
                Email = "Juan@ma",
                Money = 20.55m,
                Phone = "+5491154762312",
                UserType = "Normal"
            };

            var result = await Helpers.PostAsync<Result>(_client, "/api/users/create", command);
            Assert.Contains($"'Email' is not valid Email address.", result.Errors);
        }

        [Fact]
        public async Task GivenWrongMoneyCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateUserCommand
            {
                Name = "Luis",
                Address = "Santiago 20 Ovalo San Juan",
                Email = "lus.lopez@ma.mx",
                Money = -0112,
                Phone = "+5491154762312",
                UserType = "Normal"
            };

            var result = await Helpers.PostAsync<Result>(_client, "/api/users/create", command);
            Assert.Contains($"'Money' must be greater than '0'.", result.Errors);
        }

        [Fact]
        public async Task GivenUserWithoutUserTypeCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateUserCommand
            {
                Name = "Gian Piero",
                Address = "Lima Av Arequipa 2464",
                Email = "gian221@gmail.com",
                Money = 1234,
                Phone = "+5491154762312",
                UserType = string.Empty
            };

            var result = await Helpers.PostAsync<Result>(_client, "/api/users/create", command);
            Assert.Contains($"'User Type' must not be empty.", result.Errors);
        }
    }
}

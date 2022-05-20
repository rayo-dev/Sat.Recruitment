using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Interfaces;
using System;

namespace Sat.Recruitment.WebAPI.Tests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .BuildServiceProvider();

                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var configuration = scopedServices.GetRequiredService<IConfiguration>();
                var userFile = scopedServices.GetRequiredService<IUserFile>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                try
                {
                    // Seed the plainText with test data.
                    Helpers.InitializeDbForTests(configuration, userFile);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                                        $"database with test messages. Error: {ex.Message}");
                }
            });
        }
    }
}

using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Infrastructure.Files;

namespace Sat.Recruitment.WebAPI.Tests.Common
{
    public class Helpers
    {
        public static void InitializeDbForTests(IConfiguration configuration)
        {
            User user1 = new User()
            {
                Name = "Juan",
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Phone = "+5491154762312",
                UserType = "Normal"
            };

            User user2 = new User()
            {
                Name = "Franco",
                Address = "Alvear y Colombres",
                Email = "Franco.Perez@gmail.com",
                Money = 112234,
                Phone = "+534645213542",
                UserType = "Premium"
            };

            User user3 = new User()
            {
                Name = "Agustina",
                Address = "Garay y Otra Calle",
                Email = "Agustina@gmail.com",
                Money = 112234,
                Phone = "+534645213542",
                UserType = "SuperUser"
            };

            //CsvFileBuilder.WriteUsersFile(new[] { user1, user2, user3 });
        }
    }
}

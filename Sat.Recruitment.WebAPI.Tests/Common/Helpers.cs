using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Core.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.WebAPI.Tests.Common
{
    public class Helpers
    {
        public static void InitializeDbForTests(IConfiguration configuration, IUserFile userFile)
        {
            var list = new List<User>();

            User user1 = new User()
            {
                Name = "Juan",
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Phone = "+5491154762312",
                UserType = "Normal"
            };
            list.Add(user1);

            User user2 = new User()
            {
                Name = "Franco",
                Address = "Alvear y Colombres",
                Email = "Franco.Perez@gmail.com",
                Money = 112234,
                Phone = "+534645213542",
                UserType = "Premium"
            };
            list.Add(user2);

            User user3 = new User()
            {
                Name = "Agustina",
                Address = "Garay y Otra Calle",
                Email = "Agustina@gmail.com",
                Money = 112234,
                Phone = "+534645213542",
                UserType = "SuperUser"
            };
            list.Add(user3);

            string userPath = configuration.GetSection("Paths").GetSection("UserFile").Value;
            userFile.WriteUsersFile(userPath, list);
        }

        public static async Task<T> PostAsync<T>(HttpClient client, string url, object request)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content).Result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<T>(result);
                return response;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

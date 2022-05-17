using System.Collections.Generic;
using System.IO;
using Sat.Recruitment.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Sat.Recruitment.Infrastructure.Files
{
    public static class CsvFileBuilder
    {
        private static string userPath = string.Empty;

        public static void SetConfiguration(IConfiguration configuration)
        {
            userPath = configuration.GetSection("Paths").GetSection("UserFile").Value;
        }

        public static void DeleteUsersFile()
        {
            var filePath = userPath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static IEnumerable<User> GetUsersFile()
        {
            var filePath = userPath;
            var users = new List<User>();

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line = reader.ReadLineAsync().Result;
                    while (line != null)
                    {
                        var lineSeparated = line.Split(',');
                        var userToAdd = new User
                        {
                            Name = lineSeparated[(int)UserFileRecordEnum.Name],
                            Email = lineSeparated[(int)UserFileRecordEnum.Email],
                            Phone = lineSeparated[(int)UserFileRecordEnum.Phone],
                            Address = lineSeparated[(int)UserFileRecordEnum.Address],
                            UserType = lineSeparated[(int)UserFileRecordEnum.UserType],
                            Money = decimal.Parse(lineSeparated[(int)UserFileRecordEnum.Money]),
                        };
                        users.Add(userToAdd);
                    }
                }
            }
            return users;
        }

        public static void WriteUsersFile(IEnumerable<User> users)
        {
            var filePath = userPath;

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    foreach (var r in users)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(r.Name + ',');
                        sb.Append(r.Address + ',');
                        sb.Append(r.Phone + ',');
                        sb.Append(r.Phone + ',');
                        sb.Append(r.UserType + ',');
                        sb.Append(r.Money);

                        writer.WriteLine(sb.ToString());
                    }
                }
            }
        }
    }
}

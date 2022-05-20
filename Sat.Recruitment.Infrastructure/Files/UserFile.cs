using System.Collections.Generic;
using Sat.Recruitment.Core.Entities;
using System.Text;
using Sat.Recruitment.Application.Interfaces;

namespace Sat.Recruitment.Infrastructure.Files
{
    public class UserFile : CsvFileBuilder, IUserFile
    {
        public IEnumerable<User> GetDataUsersFile(string filePath)
        {
            var users = new List<User>();

            var lines = GetDataFile(filePath).Result;
            foreach (string line in lines)
            {
                var lineSeparated = line.Split(',', System.StringSplitOptions.None);
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
            return users;
        }

        public int WriteUsersFile(string filePath, IEnumerable<User> users)
        {
            int result = 0;
            char separator = ',';
            var lines = new List<string>();
            foreach (var r in users)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(r.Name + separator);
                sb.Append(r.Email + separator);
                sb.Append(r.Phone + separator);
                sb.Append(r.Address + separator);
                sb.Append(r.UserType + separator);
                sb.Append(r.Money);

                lines.Add(sb.ToString());
            }
            result = WriteFile(filePath, lines).Result;

            return result;
        }
    }
}

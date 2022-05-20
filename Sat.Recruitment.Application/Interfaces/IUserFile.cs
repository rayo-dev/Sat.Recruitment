using Sat.Recruitment.Core.Entities;
using System.Collections.Generic;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserFile
    {
        IEnumerable<User> GetDataUsersFile(string path);
        int WriteUsersFile(string path, IEnumerable<User> lines);
    }
}

using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<Core.Entities.User>> Search(User user);
    }
}

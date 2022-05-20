using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Infrastructure.Data;
using Sat.Recruitment.Infrastructure.Files;
using Sat.Recruitment.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Repositories
{
    public class UserRepository : Repository<Core.Entities.User>, IUserRepository
    {
        public UserRepository(SATContext employeeContext) : base(employeeContext)
        {
        }

        public async Task<IEnumerable<Core.Entities.User>> Search(User user)
        {
            return await _employeeContext.Users
                .Where(u => u.Equals(user))
                .ToListAsync();
        }


        public override async Task<User> AddAsync(User user)
        {
            var result = await base.AddAsync(user);
            return result;
        }
    }
}

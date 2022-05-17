using Microsoft.EntityFrameworkCore;

namespace Sat.Recruitment.Infrastructure.Data
{
    public class SATContext : DbContext
    {
        public SATContext(DbContextOptions<SATContext> options) : base (options)
        {

        }

        public DbSet<Core.Entities.User> Users { get; set; }
    }
}

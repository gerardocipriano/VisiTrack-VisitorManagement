using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Core.Services.Shared;

namespace Core.Services
{
    public class VisitrackDbContext : DbContext
    {
        public VisitrackDbContext()
        {
        }

        public VisitrackDbContext(DbContextOptions<VisitrackDbContext> options) : base(options)
        {
            DataGenerator.InitializeUsers(this);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
    }
}

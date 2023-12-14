using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Core.Services.Shared;

namespace Core.Services
{
    public class TemplateDbContext : DbContext
    {
        public TemplateDbContext()
        {
        }

        public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
        {
            DataGenerator.InitializeUsers(this);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
    }
}

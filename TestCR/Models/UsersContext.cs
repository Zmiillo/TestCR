using Microsoft.EntityFrameworkCore;

namespace TestCR.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
         //   Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public sealed class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;

namespace EndoSoftAssignment.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}

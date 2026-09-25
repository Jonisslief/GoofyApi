using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bench> Benches { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}

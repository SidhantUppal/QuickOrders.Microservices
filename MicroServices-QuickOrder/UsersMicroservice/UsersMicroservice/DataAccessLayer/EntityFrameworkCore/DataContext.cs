using Microsoft.EntityFrameworkCore;
using UsersMicroservice.DomainLayer.Models;

namespace UsersMicroservice.DataAccessLayer.EntityFrameworkCore
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your entity configurations here
        }
        // Define DbSet properties for your entities
        // public DbSet<YourEntity> YourEntities { get; set; }`
        public DbSet<User> Users { get; set; }
    }
}

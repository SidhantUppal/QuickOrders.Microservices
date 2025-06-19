using ItemsMicroservice.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemsMicroservice.DataAccessLayer.EntityFrameworkCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; } = null!;

    }
}

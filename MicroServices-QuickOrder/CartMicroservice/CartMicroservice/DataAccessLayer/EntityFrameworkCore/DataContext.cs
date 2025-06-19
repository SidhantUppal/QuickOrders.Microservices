using CartMicroservice.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CartMicroservice.DataAccessLayer.EntityFrameworkCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
    }
}

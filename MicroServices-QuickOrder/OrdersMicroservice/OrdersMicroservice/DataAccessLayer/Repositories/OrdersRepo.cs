using ItemsMicroservice.ApplicationLayer.RepositoriesContract; // Import repository contract interface
using ItemsMicroservice.DataAccessLayer.EntityFrameworkCore; // Import Entity Framework Core data context
using ItemsMicroservice.DomainLayer.Models; // Import domain models
using Microsoft.EntityFrameworkCore; // Import EF Core

namespace ItemsMicroservice.DataAccessLayer.Repositories // Namespace for data access layer repositories
{
    public class OrdersRepo : IOrdersRepo // OrdersRepo implements IOrdersRepo interface
    {
        private readonly DataContext _context; // EF Core data context for database operations

        public OrdersRepo(DataContext context) // Constructor with dependency injection of DataContext
        {
            _context = context; // Assign injected context to private field
        }

        public async Task<Order> AddAsync(Order order) // Add a new order asynchronously
        {
            try // Try-catch for error handling
            {
                _context.Orders.Add(order); // Add order entity to context
                await _context.SaveChangesAsync(); // Save changes to database
                return order; // Return the added order
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine("Repo error in AddAsync: " + ex.Message); // Log error
                throw; // Rethrow exception
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync() // Get all orders asynchronously
        {
            try // Try-catch for error handling
            {
                return await _context.Orders.ToListAsync(); // Retrieve all orders from database
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine("Repo error in GetAllAsync: " + ex.Message); // Log error
                return Enumerable.Empty<Order>(); // Return empty list on error
            }
        }

        public async Task<Order?> GetByIdAsync(int id) // Get order by order ID asynchronously
        {
            try // Try-catch for error handling
            {
                return await _context.Orders.FindAsync(id); // Find order by primary key
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine("Repo error in GetByIdAsync: " + ex.Message); // Log error
                return null; // Return null on error
            }
        }

        public async Task<List<Order>?> GetUserOrdersAsync(int userId) // Get all orders for a specific user asynchronously
        {
            try // Try-catch for error handling
            {
                var userOrders = await _context.Orders.Where(order => order.UserId == userId).ToListAsync(); // Query orders by user ID
                return userOrders; // Return list of user orders
            }
            catch(Exception ex) // Catch any exception
            {
                Console.WriteLine("Repo error in GetUserOrders " + ex); // Log error
                throw; // Rethrow exception
            }
        }
    }
}


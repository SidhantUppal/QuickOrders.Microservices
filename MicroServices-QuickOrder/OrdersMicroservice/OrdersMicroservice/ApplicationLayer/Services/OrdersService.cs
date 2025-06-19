using ItemsMicroservice.ApplicationLayer.Dto; // Import DTOs for order requests and responses
using ItemsMicroservice.ApplicationLayer.RepositoriesContract; // Import repository contract interface
using ItemsMicroservice.ApplicationLayer.ServicesContract; // Import service contract interface
using ItemsMicroservice.DomainLayer.Models; // Import domain models

namespace ItemsMicroservice.ApplicationLayer.Services // Namespace for service layer
{
    public class OrdersService : ServicesContract.IOrdersService // OrdersService implements IOrdersService interface
    {
        private readonly IOrdersRepo _repo; // Repository for order data access

        public OrdersService(IOrdersRepo repo) // Constructor with dependency injection of repository
        {
            this._repo = repo; // Assign injected repository to private field
        }

        public async Task<OrderResponse> CreateAsync(OrderRequest request) // Create a new order asynchronously
        {
            try // Try-catch for error handling
            {
                var order = new Order // Create new Order object
                {
                    UserId = request.UserId, // Set user ID from request
                    ItemId = request.ItemId, // Set item ID from request
                    Quantity = request.Quantity // Set quantity from request
                };

                var saved = await _repo.AddAsync(order); // Save order using repository
                return new OrderResponse // Return response DTO
                {
                    Id = saved.Id, // Set order ID
                    UserId = saved.UserId, // Set user ID
                    ItemId = saved.ItemId, // Set item ID
                    Quantity = saved.Quantity, // Set quantity
                    CreatedAt = saved.CreatedAt // Set creation timestamp
                };
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine("Service error in CreateAsync: " + ex.Message); // Log error
                throw; // Rethrow exception
            }
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync() // Get all orders asynchronously
        {
            try // Try-catch for error handling
            {
                var list = await _repo.GetAllAsync(); // Retrieve all orders from repository
                return list.Select(o => new OrderResponse // Map each order to response DTO
                {
                    Id = o.Id, // Set order ID
                    UserId = o.UserId, // Set user ID
                    ItemId = o.ItemId, // Set item ID
                    Quantity = o.Quantity, // Set quantity
                    CreatedAt = o.CreatedAt // Set creation timestamp
                });
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine("Service error in GetAllAsync: " + ex.Message); // Log error
                return Enumerable.Empty<OrderResponse>(); // Return empty list on error
            }
        }

        public async Task<OrderResponse?> GetByIdAsync(int id) // Get order by ID asynchronously
        {
            try // Try-catch for error handling
            {
                var o = await _repo.GetByIdAsync(id); // Retrieve order by ID from repository
                if (o == null) return null; // Return null if not found

                return new OrderResponse // Return response DTO
                {
                    Id = o.Id, // Set order ID
                    UserId = o.UserId, // Set user ID
                    ItemId = o.ItemId, // Set item ID
                    Quantity = o.Quantity, // Set quantity
                    CreatedAt = o.CreatedAt // Set creation timestamp
                };
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine("Service error in GetByIdAsync: " + ex.Message); // Log error
                return null; // Return null on error
            }
        }

        public async Task<IEnumerable<OrderResponse>> GetCustomerOrderAsync(int userId) // Get orders for a specific user asynchronously
        {
            try // Try-catch for error handling
            {
                var list = await _repo.GetUserOrdersAsync(userId); // Retrieve user orders from repository
                return list.Select(o => new OrderResponse // Map each order to response DTO
                {
                    Id = o.Id, // Set order ID
                    UserId = o.UserId, // Set user ID
                    ItemId = o.ItemId, // Set item ID
                    Quantity = o.Quantity, // Set quantity
                    CreatedAt = o.CreatedAt // Set creation timestamp
                });
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine("Service error in GetUserOrdersAsync: " + ex.Message); // Log error
                return Enumerable.Empty<OrderResponse>(); // Return empty list on error
            }
        }
    }
}

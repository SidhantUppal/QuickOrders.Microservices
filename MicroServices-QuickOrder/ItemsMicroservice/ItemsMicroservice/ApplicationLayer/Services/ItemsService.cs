// ItemsService.cs
// Service layer for handling item-related business logic

using ItemsMicroservice.ApplicationLayer.Dto; // DTOs for item requests and responses
using ItemsMicroservice.ApplicationLayer.RepositoriesContract; // Repository contract interface
using ItemsMicroservice.ApplicationLayer.ServicesContract; // Service contract interface
using ItemsMicroservice.DomainLayer.Models; // Domain models

namespace ItemsMicroservice.ApplicationLayer.Services
{
    // Implements the IItemsService interface for item operations
    public class ItemsService : IItemsService // Fixed the circular dependency by correcting the class declaration  
    {
        private readonly IItemsRepo _repo; // Repository for item data access

        // Constructor with dependency injection for the repository
        public ItemsService(IItemsRepo repo)
        {
            this._repo = repo; // Assign repository instance
        }

        // Retrieves all items asynchronously
        public async Task<IEnumerable<ItemResponse>> GetAllItemsAsync()
        {
            try
            {
                var items = await _repo.GetAllAsync(); // Fetch all items from repository
                return items.Select(i => new ItemResponse // Map each item to ItemResponse DTO
                {
                    Id = i.Id, // Item ID
                    Name = i.Name, // Item name
                    Price = i.Price, // Item price
                    Description = i.Description, // Item description
                    ProductImage = "https://www.shutterstock.com/image-illustration/dummy-product-packs-on-white-260nw-2260279859.jpg" // Placeholder image
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Service error in GetAllItemsAsync: {ex.Message}"); // Log error
                return Enumerable.Empty<ItemResponse>(); // Return empty list on error
            }
        }

        // Adds a new item asynchronously
        public async Task<ItemResponse> AddItemAsync(ItemRequest request)
        {
            try
            {
                var item = new Item // Create new Item entity
                {
                    Name = request.Name, // Set name from request
                    Price = request.Price, // Set price from request
                    Description = request.Description // Set description from request
                };

                var saved = await _repo.AddAsync(item); // Save item to repository

                return new ItemResponse // Return saved item as ItemResponse DTO
                {
                    Id = saved.Id, // Saved item ID
                    Name = saved.Name, // Saved item name
                    Price = saved.Price, // Saved item price
                    Description = saved.Description // Saved item description
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Service error in AddItemAsync: {ex.Message}"); // Log error
                throw; // Rethrow exception
            }
        }

        // Retrieves a single item by ID asynchronously
        public async Task<ItemResponse?> GetItemByIdAsync(int id)
        {
            try
            {
                var item = await _repo.GetById(id); // Fetch item by ID from repository
                if (item == null) return null; // Return null if not found

                return new ItemResponse // Map found item to ItemResponse DTO
                {
                    Id = item.Id, // Item ID
                    Name = item.Name, // Item name
                    Price = item.Price, // Item price
                    Description = item.Description, // Item description
                    ProductImage = "https://www.shutterstock.com/image-illustration/dummy-product-packs-on-white-260nw-2260279859.jpg" // Placeholder image
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Service error in GetItemByIdAsync: {ex.Message}"); // Log error
                return null; // Return null on error
            }
        }
    }
}

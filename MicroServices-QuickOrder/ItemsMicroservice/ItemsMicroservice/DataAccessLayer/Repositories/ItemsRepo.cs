// ItemsRepo.cs
// Repository implementation for item data access using Entity Framework Core

using ItemsMicroservice.ApplicationLayer.RepositoriesContract; // Interface for item repository
using ItemsMicroservice.DataAccessLayer.EntityFrameworkCore; // Data context for EF Core
using ItemsMicroservice.DomainLayer.Models; // Item domain model
using Microsoft.EntityFrameworkCore; // EF Core namespace

namespace ItemsMicroservice.DataAccessLayer.Repositories
{
    // Implements IItemsRepo for CRUD operations on items
    public class ItemsRepo : IItemsRepo
    {
        private readonly DataContext _context; // EF Core data context

        // Constructor with dependency injection for DataContext
        public ItemsRepo(DataContext context)
        {
            _context = context; // Assign context instance
        }

        // Retrieves all items asynchronously from the database
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            try
            {
                return await _context.Items.ToListAsync(); // Fetch all items as a list
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Repository error in GetAllAsync: {ex.Message}"); // Log error
                return Enumerable.Empty<Item>(); // Return empty list on error
            }
        }

        // Adds a new item to the database asynchronously
        public async Task<Item> AddAsync(Item item)
        {
            try
            {
                _context.Items.Add(item); // Add item to context
                await _context.SaveChangesAsync(); // Save changes to database
                return item; // Return the added item
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Repository error in AddAsync: {ex.Message}"); // Log error
                throw; // Rethrow exception
            }
        }

        // Retrieves a single item by ID asynchronously
        public Task<Item?> GetById(int id)
        {
            try
            {
               return _context.Items.FirstOrDefaultAsync(i=>i.Id == id); // Find item by ID
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Repository error in getitembyud: {ex.Message}"); // Log error
                throw; // Rethrow exception
            }
        }
    }

}


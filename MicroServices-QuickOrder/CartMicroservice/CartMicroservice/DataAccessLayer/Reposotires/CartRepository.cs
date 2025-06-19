using CartMicroservice.ApplicationLayer.RepositoriesContract; // Import repository contract interface
using CartMicroservice.DataAccessLayer.EntityFrameworkCore; // Import EF Core data context
using CartMicroservice.DomainLayer.Models; // Import domain models
using Microsoft.EntityFrameworkCore; // Import EF Core

namespace CartMicroservice.DataAccessLayer.Reposotires
{
    // Repository class for cart data access
    public class CartRepository: ICartRepository
    {
        private readonly DataContext _context; // EF Core data context

        // Constructor with dependency injection for data context
        public CartRepository(DataContext context)
        {
            _context = context; // Assign context instance
        }

        // Retrieve a cart by user ID asynchronously
        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            var userCart = await _context.Carts // Query carts table
                .Include(c => c.Items) // Include cart items
                .FirstOrDefaultAsync(c => c.UserId == userId); // Find cart by user ID
            return userCart; // Return cart or null
        }

        // Add a new cart or update an existing cart asynchronously
        public async Task<Cart> AddOrUpdateCartAsync(Cart cart)
        {
            // Fetch the existing cart for the user from the database
            var existingCart = await _context.Carts
                .Include(c => c.Items) // Include cart items
                .FirstOrDefaultAsync(c => c.UserId == cart.UserId); // Find cart by user ID

            if (existingCart == null)
            {
                // If the cart does not exist, add the new cart as-is
                _context.Carts.Add(cart); // Add new cart
            }
            else
            {
                // Iterate over the items in the new cart
                foreach (var newItem in cart.Items)
                {
                    // Check if this item already exists in the existing cart
                    var existingItem = existingCart.Items
                        .FirstOrDefault(i => i.ProductId == newItem.ProductId); // Find item by product ID

                    if (existingItem != null)
                    {
                        // If the item exists, update the quantity and price with the new values (replace, not add)
                        existingItem.Quantity = newItem.Quantity; // Update quantity
                        existingItem.Price = newItem.Price; // Update price
                    }
                    else
                    {
                        // If the item does not exist, add it to the cart
                        existingCart.Items.Add(newItem); // Add new item
                    }
                }
                // No need to call _context.Carts.Update(existingCart) as EF Core tracks changes
            }

            // Save all changes to the database
            await _context.SaveChangesAsync(); // Save changes

            // Return the updated or newly added cart
            return existingCart ?? cart; // Return cart
        }

        // Clear all items from a user's cart asynchronously
        public async Task<bool> ClearCartAsync(string userId)
        {
            var cart = await GetCartByUserIdAsync(userId); // Get cart by user ID
            if (cart == null) return false; // Return false if cart not found

            _context.CartItems.RemoveRange(cart.Items); // Remove all items from cart
            await _context.SaveChangesAsync(); // Save changes
            return true; // Return true if cleared
        }

        // Remove a specific item from a user's cart asynchronously
        public async Task<bool> RemoveCartItemAsync(string userId, int productId)
        {
            var cart = await GetCartByUserIdAsync(userId); // Get cart by user ID
            if (cart == null) return false; // Return false if cart not found

            var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productId); // Find item by product ID
            if (itemToRemove == null) return false; // Return false if item not found

            cart.Items.Remove(itemToRemove); // Remove item from cart
            _context.Carts.Update(cart); // Update cart in context

            await _context.SaveChangesAsync(); // Save changes
            return true; // Return true if removed
        }
    }
}


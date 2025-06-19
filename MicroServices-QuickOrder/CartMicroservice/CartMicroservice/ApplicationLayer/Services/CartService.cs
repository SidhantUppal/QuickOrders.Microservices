using CartMicroservice.ApplicationLayer.Dto; // Import DTOs for cart operations
using CartMicroservice.ApplicationLayer.RepositoriesContract; // Import repository contract interface
using CartMicroservice.ApplicationLayer.ServiceContracts; // Import service contract interface
using CartMicroservice.DomainLayer.Models; // Import domain models
using Microsoft.AspNetCore.Cors.Infrastructure; // Import CORS infrastructure (not used in this file)

namespace CartMicroservice.ApplicationLayer.Services
{
    // Service class for cart-related business logic
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo; // Repository for cart data access

        // Constructor with dependency injection for repository
        public CartService(ICartRepository repo)
        {
            _repo = repo; // Assign repository instance
        }

        // Retrieve a user's cart asynchronously
        public async Task<Response<CartDto>> GetCartAsync(string userId)
        {
            var response = new Response<CartDto>(); // Initialize response object

            try
            {
                var cart = await _repo.GetCartByUserIdAsync(userId); // Fetch cart from repository

                if (cart == null)
                {
                    response.Status = false; // Set status to false if cart not found
                    response.Message = "Cart not found."; // Set not found message
                    response.Data = null; // No data
                    return response; // Return response
                }

                var cartDto = new CartDto
                {
                    UserId = cart.UserId, // Set user ID
                    Items = cart.Items.Select(item => new CartItemDto
                    {
                        ProductId = item.ProductId, // Set product ID
                        ProductName = item.ProductName, // Set product name
                        //Price = item.Price,
                        Price = item.Price * item.Quantity, // Calculate total price for item
                        Quantity = item.Quantity, // Set quantity
                        ProductImage = "https://www.shutterstock.com/image-illustration/dummy-product-packs-on-white-260nw-2260279859.jpg" // Dummy image URL
                    }).ToList() // Convert to list
                };

                response.Status = true; // Set status to true
                response.Message = "Cart retrieved successfully."; // Set success message
                response.Data = cartDto; // Set cart data
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCartAsync: {ex.Message}"); // Log error
                response.Status = false; // Set status to false
                response.Message = "An error occurred while retrieving the cart."; // Set error message
                response.Data = null; // No data
            }

            return response; // Return response
        }

        // Add an item to the user's cart asynchronously
        public async Task<Response<Cart>> AddItemAsync(string userId, CartItem item)
        {
            var response = new Response<Cart>(); // Initialize response object

            try
            {
                var cart = await _repo.GetCartByUserIdAsync(userId) ?? new Cart
                {
                    UserId = userId, // Set user ID
                    Items = new List<CartItem>() // Initialize items list
                };

                var existing = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId); // Check if item exists
                if (existing != null)
                    existing.Quantity += item.Quantity; // Increase quantity if exists
                else
                    cart.Items.Add(item); // Add new item if not exists

                var updated = await _repo.AddOrUpdateCartAsync(cart); // Update cart in repository

                response.Status = true; // Set status to true
                response.Message = "Item added successfully."; // Set success message
                response.Data = updated; // Set updated cart data
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddItemAsync: {ex.Message}"); // Log error
                response.Status = false; // Set status to false
                response.Message = "An error occurred while adding the item to the cart."; // Set error message
                response.Data = null; // No data
            }

            return response; // Return response
        }

        // Clear all items from the user's cart asynchronously
        public async Task<Response<bool>> ClearCartAsync(string userId)
        {
            var response = new Response<bool>(); // Initialize response object

            try
            {
                var cleared = await _repo.ClearCartAsync(userId); // Clear cart in repository

                response.Status = cleared; // Set status based on result
                response.Message = cleared ? "Cart cleared successfully." : "Cart not found."; // Set message
                response.Data = cleared; // Set result data
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ClearCartAsync: {ex.Message}"); // Log error
                response.Status = false; // Set status to false
                response.Message = "An error occurred while clearing the cart."; // Set error message
                response.Data = false; // Set data to false
            }

            return response; // Return response
        }

        // Remove a specific item from the user's cart asynchronously
        public async Task<Response<bool>> RemoveCartItemAsync(string userId, int productId)
        {
            var response = new Response<bool>(); // Initialize response object

            try
            {
                var success = await _repo.RemoveCartItemAsync(userId, productId); // Remove item from repository

                if (!success)
                {
                    response.Status = false; // Set status to false if not found
                    response.Message = "Item not found in cart or cart does not exist."; // Set not found message
                    response.Data = false; // Set data to false
                }
                else
                {
                    response.Status = true; // Set status to true
                    response.Message = "Item removed from cart successfully."; // Set success message
                    response.Data = true; // Set data to true
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Service error in RemoveCartItemAsync: {ex.Message}"); // Log error
                response.Status = false; // Set status to false
                response.Message = "An error occurred while removing the item."; // Set error message
                response.Data = false; // Set data to false
            }

            return response; // Return response
        }
    }
}

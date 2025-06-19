using CartMicroservice.ApplicationLayer.Dto; // Import DTOs for cart operations
using CartMicroservice.ApplicationLayer.ServiceContracts; // Import service contract interface
using CartMicroservice.DomainLayer.Models; // Import domain models
using Microsoft.AspNetCore.Http; // Import HTTP features
using Microsoft.AspNetCore.Mvc; // Import MVC features

namespace CartMicroservice.Controllers
{
    [Route("api/[controller]")] // Set route prefix for controller
    [ApiController] // Mark as API controller
    public class CartController : ControllerBase
    {
        private readonly ICartService _service; // Service for cart business logic

        // Constructor with dependency injection for cart service
        public CartController(ICartService service)
        {
            _service = service; // Assign service instance
        }

        [HttpGet("getusercart/{userId}")] // GET endpoint for retrieving user cart
        public async Task<IActionResult> GetCart(string userId)
        {
            try
            {
                var result = await _service.GetCartAsync(userId); // Call service to get cart
                return Ok(result); // Return result with 200 OK
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller error in GetCart: {ex.Message}"); // Log error
                var response = new Response<Cart>
                {
                    Status = false, // Set status to false
                    Message = "Controller error in GetCart: " + ex.Message, // Set error message
                    Data = null // No data
                };
                return Ok(response); // Return error response with 200 OK
            }
        }

        [HttpPost("addcartitem/{userId}")] // POST endpoint for adding item to cart
        public async Task<IActionResult> AddItem(string userId, [FromBody] CartRequest request)
        {
            try
            {
                var cartItem = new CartItem
                {
                    ProductId = request.ProductId, // Set product ID
                    ProductName = request.ProductName, // Set product name
                    Price = request.Price, // Set price
                    Quantity = request.Quantity // Set quantity
                };

                var result = await _service.AddItemAsync(userId, cartItem); // Call service to add item
                return Ok(result); // Return result with 200 OK
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller error in AddItem: {ex.Message}"); // Log error
                var response = new Response<Cart>
                {
                    Status = false, // Set status to false
                    Message = "Controller error in AddItem: " + ex.Message, // Set error message
                    Data = null // No data
                };
                return Ok(response); // Return error response with 200 OK
            }
        }

        [HttpDelete("clearusercart/{userId}")] // DELETE endpoint for clearing user cart
        public async Task<IActionResult> ClearCart(string userId)
        {
            try
            {
                var result = await _service.ClearCartAsync(userId); // Call service to clear cart
                return Ok(result); // Return result with 200 OK
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller error in ClearCart: {ex.Message}"); // Log error
                var response = new Response<bool>
                {
                    Status = false, // Set status to false
                    Message = "Controller error in ClearCart: " + ex.Message, // Set error message
                    Data = false // Set data to false
                };
                return Ok(response); // Return error response with 200 OK
            }
        }

        // DELETE endpoint for removing a specific item from the cart
        [HttpDelete("removeitem/{userId}/{productId}")]
        public async Task<IActionResult> RemoveItem(string userId, int productId)
        {
            try
            {
                var result = await _service.RemoveCartItemAsync(userId, productId); // Call service to remove item
                return Ok(result); // Return result with 200 OK
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller error in RemoveItem: {ex.Message}"); // Log error
                var response = new Response<bool>
                {
                    Status = false, // Set status to false
                    Message = "Controller error in RemoveItem: " + ex.Message, // Set error message
                    Data = false // Set data to false
                };
                return Ok(response); // Return error response with 200 OK
            }
        }
    }
}


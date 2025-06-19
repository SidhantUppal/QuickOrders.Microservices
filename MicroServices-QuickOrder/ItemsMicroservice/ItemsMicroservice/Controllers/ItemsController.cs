// ItemsController.cs
// Controller for handling item-related API endpoints

using ItemsMicroservice.ApplicationLayer.Dto; // DTOs for item requests and responses
using ItemsMicroservice.ApplicationLayer.ServicesContract; // Service contract interface
using Microsoft.AspNetCore.Http; // Provides HTTP features for ASP.NET Core
using Microsoft.AspNetCore.Mvc; // Provides MVC features for ASP.NET Core

namespace ItemsMicroservice.Controllers
{
    [Route("api/[controller]")] // Route for the controller: api/items
    [ApiController] // Indicates this is an API controller
    public class ItemsController : ControllerBase // Inherits from ControllerBase for API functionality
    {
        private readonly IItemsService _service; // Service for item business logic

        // Constructor with dependency injection for IItemsService
        public ItemsController(IItemsService service)
        {
            _service = service; // Assign service instance
        }

        [HttpGet("getallitems")] // GET: api/items/getallitems
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllItemsAsync(); // Retrieve all items from service
                var response = new Response<IEnumerable<ItemResponse>>(); // Prepare response object

                if (result is not null && result.Any()) // If items exist
                {
                    response.Status = true; // Set status to true
                    response.Message = "Items retrieved successfully."; // Success message
                    response.Data = result; // Set data to items
                    return Ok(response); // Return 200 OK with response
                }
                else
                {
                    response.Status = false; // Set status to false
                    response.Message = "No items found."; // Not found message
                    response.Data = null; // No data
                    return NotFound(response); // Return 404 Not Found with response
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller error in GetAll: {ex.Message}"); // Log error
                var errorResponse = new Response<IEnumerable<ItemResponse>>
                {
                    Status = false, // Set status to false
                    Message = "An error occurred while retrieving items.", // Error message
                    Data = null // No data
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error
            }
        }

        [HttpPost("additem")] // POST: api/items/additem
        public async Task<IActionResult> AddItem([FromBody] ItemRequest request)
        {
            if (!ModelState.IsValid) // Validate model state
            {
                var validationResponse = new Response<ItemResponse>
                {
                    Status = false, // Set status to false
                    Message = "Invalid item data.", // Validation error message
                    Data = null // No data
                };
                return BadRequest(validationResponse); // Return 400 Bad Request
            }

            try
            {
                var added = await _service.AddItemAsync(request); // Add item via service
                var response = new Response<ItemResponse>
                {
                    Status = true, // Set status to true
                    Message = "Item added successfully.", // Success message
                    Data = added // Set data to added item
                };
                return CreatedAtAction(nameof(GetById), new { id = added.Id }, response); // Return 201 Created
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller error in AddItem: {ex.Message}"); // Log error
                var errorResponse = new Response<ItemResponse>
                {
                    Status = false, // Set status to false
                    Message = "An error occurred while adding the item.", // Error message
                    Data = null // No data
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error
            }
        }

        [HttpGet("getitem/{id}")] // GET: api/items/getitem/{id}
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetItemByIdAsync(id); // Retrieve item by ID from service
                var response = new Response<ItemResponse>(); // Prepare response object

                if (item is not null) // If item exists
                {
                    response.Status = true; // Set status to true
                    response.Message = "Item retrieved successfully."; // Success message
                    response.Data = item; // Set data to item
                    return Ok(response); // Return 200 OK with response
                }
                else
                {
                    response.Status = false; // Set status to false
                    response.Message = "Item not found."; // Not found message
                    response.Data = null; // No data
                    return NotFound(response); // Return 404 Not Found with response
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller error in GetById: {ex.Message}"); // Log error
                var errorResponse = new Response<ItemResponse>
                {
                    Status = false, // Set status to false
                    Message = "An error occurred while retrieving the item.", // Error message
                    Data = null // No data
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error
            }
        }
    }

}

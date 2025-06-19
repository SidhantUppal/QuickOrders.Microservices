using ItemsMicroservice.ApplicationLayer.Dto; // Import DTOs for order requests and responses
using ItemsMicroservice.ApplicationLayer.ServicesContract; // Import service contract interface
using ItemsMicroservice.DomainLayer.Models; // Import domain models
using Microsoft.AspNetCore.Authorization; // Import for authorization attributes
using Microsoft.AspNetCore.Http; // Import for HTTP context
using Microsoft.AspNetCore.Mvc; // Import for MVC controller base
using OrdersMicroservice.DomainLayer.Models; // Import response model

namespace OrdersMicroservice.Controllers // Namespace for controllers
{
    [Route("api/[controller]")] // Route for API controller
    [ApiController] // Marks this class as an API controller
    public class OrdersController : ControllerBase // OrdersController inherits from ControllerBase
    {
        private readonly IOrdersService _service; // Service for order operations

        public OrdersController(IOrdersService service) // Constructor with dependency injection of service
        {
            _service = service; // Assign injected service to private field
        }

      

        //[Authorize]

        [HttpPost] // HTTP POST endpoint for creating an order
        public async Task<IActionResult> Create([FromBody] OrderRequest request) // Create order action
        {
            if (!ModelState.IsValid) // Check if request model is valid
            {
                var validationResponse = new Response<OrderResponse> // Prepare validation error response
                {
                    Status = false, // Set status to false
                    Message = "Invalid request data.", // Set error message
                    Data = null // No data
                };
                return BadRequest(validationResponse); // Return 400 Bad Request
            }

            try // Try-catch for error handling
            {
                var result = await _service.CreateAsync(request); // Call service to create order
                var response = new Response<OrderResponse> // Prepare success response
                {
                    Status = true, // Set status to true
                    Message = "Order created successfully.", // Set success message
                    Data = result // Set created order data
                };
                return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, response); // Return 201 Created
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine($"Error in Create: {ex.Message}"); // Log error
                var errorResponse = new Response<OrderResponse> // Prepare error response
                {
                    Status = false, // Set status to false
                    Message = "An error occurred while creating the order.", // Set error message
                    Data = null // No data
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error
            }
        }

        [HttpGet] // HTTP GET endpoint for retrieving all orders
        public async Task<IActionResult> GetAll() // Get all orders action
        {
            try // Try-catch for error handling
            {
                var result = await _service.GetAllAsync(); // Call service to get all orders
                var response = new Response<IEnumerable<OrderResponse>>(); // Prepare response object

                if (result is not null && result.Any()) // If orders exist
                {
                    response.Status = true; // Set status to true
                    response.Message = "Orders retrieved successfully."; // Set success message
                    response.Data = result; // Set orders data
                    return Ok(response); // Return 200 OK
                }
                else // If no orders found
                {
                    response.Status = false; // Set status to false
                    response.Message = "No orders found."; // Set not found message
                    response.Data = null; // No data
                    return NotFound(response); // Return 404 Not Found
                }
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine($"Error in GetAll: {ex.Message}"); // Log error
                var errorResponse = new Response<IEnumerable<OrderResponse>>
                {
                    Status = false, // Set status to false
                    Message = "An error occurred while retrieving orders.", // Set error message
                    Data = null // No data
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error
            }
        }

        [HttpGet("{id}")] // HTTP GET endpoint for retrieving order by ID
        public async Task<IActionResult> GetById(int id) // Get order by ID action
        {
            try // Try-catch for error handling
            {
                var order = await _service.GetByIdAsync(id); // Call service to get order by ID
                if (order == null) // If order not found
                    return NotFound(); // Return 404 Not Found

                return Ok(order); // Return 200 OK with order
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine($"Error in GetById: {ex.Message}"); // Log error
                return StatusCode(500, "An error occurred while retrieving the order."); // Return 500 Internal Server Error
            }
        }

        [HttpGet("getorderdetails/{id}")] // HTTP GET endpoint for retrieving order details by ID
        public async Task<IActionResult> GetOrderById(int id) // Get order details by ID action
        {
            try // Try-catch for error handling
            {
                var order = await _service.GetByIdAsync(id); // Call service to get order by ID
                var response = new Response<OrderResponse>(); // Prepare response object

                if (order is not null) // If order found
                {
                    response.Status = true; // Set status to true
                    response.Message = "Order retrieved successfully."; // Set success message
                    response.Data = order; // Set order data
                    return Ok(response); // Return 200 OK
                }
                else // If order not found
                {
                    response.Status = false; // Set status to false
                    response.Message = "Order not found."; // Set not found message
                    response.Data = null; // No data
                    return Ok(response); // Return 200 OK with not found response
                }
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine($"Error in GetOrderById: {ex.Message}"); // Log error
                var errorResponse = new Response<OrderResponse>
                {
                    Status = false, // Set status to false
                    Message = "An error occurred while retrieving the order.", // Set error message
                    Data = null // No data
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error
            }
        }

        [HttpGet("getuserorder/{id}")] // HTTP GET endpoint for retrieving orders by user ID
        public async Task<IActionResult> GetUserById(int id) // Get orders by user ID action
        {
            try // Try-catch for error handling
            {
                var response = new Response<IEnumerable<OrderResponse>>(); // Prepare response object
                var orders = await _service.GetCustomerOrderAsync(id); // Call service to get user orders

                if (orders is not null && orders.Any()) // If orders found
                {
                    response.Data = orders; // Set orders data
                    response.Status = true; // Set status to true
                    response.Message = "Orders retrieved successfully."; // Set success message
                    return Ok(response); // Return 200 OK
                }
                else // If no orders found
                {
                    response.Status = false; // Set status to false
                    response.Message = "No orders found for the given ID."; // Set not found message
                    response.Data = null; // No data
                    return NotFound(response); // Return 404 Not Found
                }
            }
            catch (Exception ex) // Catch any exception
            {
                Console.WriteLine($"Error in GetUserById: {ex.Message}"); // Log error
                var errorResponse = new Response<IEnumerable<OrderResponse>>
                {
                    Status = false, // Set status to false
                    Message = "An error occurred while retrieving the order.", // Set error message
                    Data = null // No data
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UsersMicroservice.ApplicationLayer.Dto;
using UsersMicroservice.ApplicationLayer.ServiceContract;
using UsersMicroservice.DomainLayer.Models;

namespace UsersMicroservice.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUsersService _userService;

        public AccountController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                var validationResponse = new Response<User>
                {
                    Status = false,
                    Message = "Invalid registration data.",
                    Data = null
                };
                return BadRequest(validationResponse);
            }

            try
            {
                var result = await _userService.RegisterAsync(request);
                if (result == null || result.User == null)
                {
                    var conflictResponse = new Response<RegisterRequest>
                    {
                        Status = false,
                        Message = "Email already exists.",
                        Data = null
                    };
                    return Conflict(conflictResponse);
                }

                var response = new Response<User>
                {
                    Status = true,
                    Message = "User registered successfully.",
                    Data = result.User
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<User>
                {
                    Status = false,
                    Message = $"Registration error: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                var validationResponse = new Response<User>
                {
                    Status = false,
                    Message = "Invalid login data.",
                    Data = null
                };
                return Ok(validationResponse);
            }

            try
            {
                var result = await _userService.LoginAsync(request);
                if (result == null || result.User == null)
                {
                    var unauthorizedResponse = new Response<User>
                    {
                        Status = false,
                        Message = "Invalid email or password.",
                        Data = null
                    };
                    return Ok(unauthorizedResponse);
                }

                var response = new Response<User>
                {
                    Status = true,
                    Message = "Login successful.",
                    Data = result.User,
                };
                response.Data.Token = result.Token;
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<User>
                {
                    Status = false,
                    Message = $"Login error: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var response = new Response<IEnumerable<User>>();

                if (users is not null && users.Any())
                {
                    response.Status = true;
                    response.Message = "Users retrieved successfully.";
                    response.Data = users;
                    return Ok(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "No users found.";
                    response.Data = null;
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<IEnumerable<User>>
                {
                    Status = false,
                    Message = $"Error retrieving users: {ex.Message}",
                    Data = null
                };
                return StatusCode(500, errorResponse);
            }
        }
    }





    //[ApiController]
    //[Route("api/[controller]")]
    //public class AccountController : ControllerBase
    //{
    //    private readonly IUsersService _userService;

    //    public AccountController(IUsersService userService)
    //    {
    //        _userService = userService;
    //    }

    //    [HttpPost("register")]
    //    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        try
    //        {
    //            var result = await _userService.RegisterAsync(request);
    //            if (result == null || result.User == null)
    //            {
    //                return Conflict(new { message = "Email already exists." });
    //            }

    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log exception using ILogger in production
    //            return StatusCode(500, new { message = $"Registration error: {ex.Message}" });
    //        }
    //    }

    //    [HttpPost("login")]
    //    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        try
    //        {
    //            var result = await _userService.LoginAsync(request);
    //            if (result == null || result.User == null)
    //            {
    //                return Unauthorized(new { message = "Invalid email or password." });
    //            }

    //            return Ok(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log exception using ILogger in production
    //            return StatusCode(500, new { message = $"Login error: {ex.Message}" });
    //        }
    //    }

    //    [HttpGet("getallusers")]
    //    public async Task<IActionResult> GetAllUsers()
    //    {
    //        var users = await _userService.GetAllUsersAsync();
    //        return Ok(users);
    //    }

    //}
}

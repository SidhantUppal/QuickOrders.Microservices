using System; // Import System namespace for base types
using System.Threading.Tasks; // Import for async/await support
using UsersMicroservice.ApplicationLayer.Dto; // Import DTOs for requests and responses
using UsersMicroservice.ApplicationLayer.RepositoryContract; // Import repository contract interface
using UsersMicroservice.DomainLayer.Models; // Import User domain model
using BCrypt.Net; // Import BCrypt for password hashing
using UsersMicroservice.ApplicationLayer.Services; // Import services namespace

namespace UsersMicroservice.ApplicationLayer.ServiceContract // Define the namespace for service contracts
{
    /// <summary>
    /// Service class for user-related business logic and operations.
    /// </summary>
    public class UsersService : IUsersService // UsersService implements the IUsersService interface
    {
        private readonly IUsersRepo _usersRepo; // Private field for user repository dependency
        private readonly ITokenService _tokenService; // Private field for token service dependency

        /// <summary>
        /// Constructor for UsersService with dependency injection.
        /// </summary>
        /// <param name="usersRepo">Injected user repository.</param>
        /// <param name="tokenService">Injected token service.</param>
        public UsersService(IUsersRepo usersRepo, ITokenService tokenService) // Constructor receives dependencies
        {
            _usersRepo = usersRepo; // Assign injected user repository to private field
            _tokenService = tokenService; // Assign injected token service to private field
        }

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="request">Registration request DTO.</param>
        /// <returns>Response object with registration result.</returns>
        public async Task<Response?> RegisterAsync(RegisterRequest request) // Async method to register a user
        {
            try // Start try block to handle exceptions
            {
                var existing = await _usersRepo.GetByEmailAsync(request.Email); // Check if user with email exists
                if (existing != null) // If user already exists
                {
                    return new Response // Return response indicating email is already registered
                    {
                        Message = "Email already registered."
                    };
                }

                var user = new User // Create new User object
                {
                    Email = request.Email, // Set email from request
                    Username = request.Username, // Set username from request
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) // Hash password using BCrypt
                };

                var savedUser = await _usersRepo.AddUserAsync(user); // Save new user to repository

                return new Response // Return success response
                {
                    Message = "User registered successfully.",
                    User = new User // Return user info (without password)
                    {
                        Id = savedUser.Id,
                        Email = savedUser.Email,
                        Username = savedUser.Username
                    }
                    //Token = _tokenService.GenerateToken(savedUser) if needed
                };
            }
            catch (Exception ex) // Catch any exceptions that occur
            {
                // Log exception (use ILogger in production)
                return new Response { Message = $"Registration failed: {ex.Message}" }; // Return error response
            }
        }

        /// <summary>
        /// Authenticates a user and generates a token if successful.
        /// </summary>
        /// <param name="request">Login request DTO.</param>
        /// <returns>Response object with login result and token if successful.</returns>
        public async Task<Response?> LoginAsync(LoginRequest request) // Async method to login a user
        {
            try // Start try block to handle exceptions
            {
                var user = await _usersRepo.GetByEmailAsync(request.Email); // Retrieve user by email
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) // Check user existence and password
                {
                    return new Response // Return response for invalid credentials
                    {
                        Message = "Invalid email or password."
                    };
                }

                return new Response // Return success response with token
                {
                    Message = "Login successful.",
                    User = new User // Return user info (without password)
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Username = user.Username
                    },
                    Token = _tokenService.GenerateToken(user) // Generate JWT or similar token
                };
            }
            catch (Exception ex) // Catch any exceptions that occur
            {
                // Log exception (use ILogger in production)
                return new Response { Message = $"Login failed: {ex.Message}" }; // Return error response
            }
        }

        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>Enumerable of User objects.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync() // Async method to get all users
        {
            try // Start try block to handle exceptions
            {
                return await _usersRepo.GetAllUsersAsync(); // Retrieve all users from repository
            }
            catch (Exception ex) // Catch any exceptions that occur
            {
                Console.WriteLine($"Service error: {ex.Message}"); // Print error message to console
                return Enumerable.Empty<User>(); // Return empty enumerable on error
            }
        }

    }
}

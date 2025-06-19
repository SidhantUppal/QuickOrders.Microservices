using Microsoft.EntityFrameworkCore; // Import Entity Framework Core for database operations
using UsersMicroservice.ApplicationLayer.RepositoryContract; // Import repository contract interface
using UsersMicroservice.DataAccessLayer.EntityFrameworkCore; // Import DataContext for EF Core
using UsersMicroservice.DomainLayer.Models; // Import User domain model

namespace UsersMicroservice.DataAccessLayer.Repositories // Define the namespace for the repository
{
    public class UsersRepo : IUsersRepo // UsersRepo implements the IUsersRepo interface
    {
        private readonly DataContext _context; // Private field to hold the database context

        public UsersRepo(DataContext context) // Constructor with dependency injection of DataContext
        {
            _context = context; // Assign the injected context to the private field
        }

        public async Task<User?> GetByEmailAsync(string email) // Async method to get a user by email
        {
            try // Start try block to handle exceptions
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email); // Query the Users DbSet for a user with the specified email
            }
            catch (Exception ex) // Catch any exceptions that occur
            {
                // Log error (use ILogger in real-world apps)
                Console.WriteLine($"Error fetching user by email: {ex.Message}"); // Print error message to console
                return null; // Return null if an error occurs
            }
        }

        public async Task<User> AddUserAsync(User user) // Async method to add a new user
        {
            try // Start try block to handle exceptions
            {
                _context.Users.Add(user); // Add the user entity to the Users DbSet
                await _context.SaveChangesAsync(); // Save changes to the database asynchronously
                return user; // Return the added user entity
            }
            catch (Exception ex) // Catch any exceptions that occur
            {
                // Log error (use ILogger in real-world apps)
                Console.WriteLine($"Error adding user: {ex.Message}"); // Print error message to console
                throw; // Rethrow the exception to be handled at a higher layer
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() // Async method to get all users
        {
            try // Start try block to handle exceptions
            {
                var users = await _context.Users.ToListAsync(); // Retrieve all users as a list asynchronously

                // Remove password hash for safety
                foreach (var user in users) // Iterate through each user in the list
                {
                    user.PasswordHash = null!; // Set the PasswordHash property to null for security
                }

                return users; // Return the list of users
            }
            catch (Exception ex) // Catch any exceptions that occur
            {
                Console.WriteLine($"Error retrieving all users: {ex.Message}"); // Print error message to console
                return Enumerable.Empty<User>(); // Return an empty enumerable if an error occurs
            }
        }
    }
}

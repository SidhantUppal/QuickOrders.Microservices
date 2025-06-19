using UsersMicroservice.DomainLayer.Models;

namespace UsersMicroservice.ApplicationLayer.RepositoryContract
{
    public interface IUsersRepo
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddUserAsync(User user);

        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}

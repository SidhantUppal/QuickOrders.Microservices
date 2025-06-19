using UsersMicroservice.ApplicationLayer.Dto;
using UsersMicroservice.DomainLayer.Models;

namespace UsersMicroservice.ApplicationLayer.ServiceContract
{
    public interface IUsersService
    {
        Task<Response?> RegisterAsync(RegisterRequest request);
        Task<Response?> LoginAsync(LoginRequest request);

        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}

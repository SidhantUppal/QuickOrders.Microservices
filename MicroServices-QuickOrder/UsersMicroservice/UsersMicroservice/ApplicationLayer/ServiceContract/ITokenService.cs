using UsersMicroservice.DomainLayer.Models;

namespace UsersMicroservice.ApplicationLayer.ServiceContract
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}

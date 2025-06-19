using CartMicroservice.ApplicationLayer.Dto;
using CartMicroservice.DomainLayer.Models;
using System.Threading.Tasks;

namespace CartMicroservice.ApplicationLayer.ServiceContracts
{
    public interface ICartService
    {
        Task<Response<CartDto>> GetCartAsync(string userId);
        Task<Response<Cart>> AddItemAsync(string userId, CartItem item);
        Task<Response<bool>> ClearCartAsync(string userId);
        Task<Response<bool>> RemoveCartItemAsync(string userId, int productId);
    }
}

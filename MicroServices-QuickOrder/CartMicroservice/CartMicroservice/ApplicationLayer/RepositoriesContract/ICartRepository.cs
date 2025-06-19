using CartMicroservice.DomainLayer.Models;

namespace CartMicroservice.ApplicationLayer.RepositoriesContract
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task<Cart> AddOrUpdateCartAsync(Cart cart);
        Task<bool> ClearCartAsync(string userId);
        Task<bool> RemoveCartItemAsync(string userId, int productId);
    }
}

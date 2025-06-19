using ItemsMicroservice.DomainLayer.Models;

namespace ItemsMicroservice.ApplicationLayer.RepositoriesContract
{
    public interface IOrdersRepo
    {
        Task<Order> AddAsync(Order order);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>?> GetUserOrdersAsync(int userId);
    }
}

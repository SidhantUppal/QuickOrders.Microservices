using ItemsMicroservice.ApplicationLayer.Dto;

namespace ItemsMicroservice.ApplicationLayer.ServicesContract
{
    public interface IOrdersService
    {
        Task<OrderResponse> CreateAsync(OrderRequest request);
        Task<IEnumerable<OrderResponse>> GetAllAsync();
        Task<OrderResponse?> GetByIdAsync(int id);
        Task<IEnumerable<OrderResponse>> GetCustomerOrderAsync(int userId);
    }
}

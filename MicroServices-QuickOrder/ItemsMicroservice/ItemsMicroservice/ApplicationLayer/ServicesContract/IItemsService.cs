using ItemsMicroservice.ApplicationLayer.Dto;

namespace ItemsMicroservice.ApplicationLayer.ServicesContract
{
    public interface IItemsService
    {
        Task<IEnumerable<ItemResponse>> GetAllItemsAsync();
        Task<ItemResponse> AddItemAsync(ItemRequest request);

        Task<ItemResponse?> GetItemByIdAsync(int id);
    }
}

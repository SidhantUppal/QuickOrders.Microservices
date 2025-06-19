using ItemsMicroservice.DomainLayer.Models;

namespace ItemsMicroservice.ApplicationLayer.RepositoriesContract
{
    public interface IItemsRepo
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> AddAsync(Item item);
        Task<Item?> GetById(int id);
    }
}

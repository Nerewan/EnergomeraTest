using Task3.Model.DTOs.Requests;
using Task3.Model.Entities;

namespace Task3.Repository.ItemRepository
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync(PaginatedRequest paginatedRequest);
        Task<Item> GetByIdAsync(int id);
        Task<int> CreateAsync(Item record);
        Task<int> UpdateAsync(Item record);
        Task DeleteAsync(int id);
    }
}

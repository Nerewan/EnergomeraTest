using Task3.Model.DTOs;
using Task3.Model.DTOs.Requests;
using Task3.Model.DTOs.Requests.Item;
using Task3.Model.DTOs.Responses.Item;

namespace Task3.Domain.ItemDomain
{
    public interface IItemDomain
    {
        Task<CommandResponse<IEnumerable<ItemResponseBase>>> GetAllAsync(PaginatedRequest paginatedRequest);
        Task<CommandResponse<ItemResponseBase>> GetByIdAsync(int userId);
        Task<CommandResponse<ItemResponseBase>> CreateAsync(CreateItemRequest createRequest);
        Task<CommandResponse> UpdateAsync(UpdateItemRequest updateRequest);
        Task<CommandResponse> DeleteAsync(int itemId);
    }
}

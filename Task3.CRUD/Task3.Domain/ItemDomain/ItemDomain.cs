using AutoMapper;
using Task3.Model.DTOs;
using Task3.Model.DTOs.Requests;
using Task3.Model.DTOs.Requests.Item;
using Task3.Model.DTOs.Responses.Item;
using Task3.Model.Entities;
using Task3.Repository.ItemRepository;

namespace Task3.Domain.ItemDomain
{
    public class ItemDomain : IItemDomain
    {

        private readonly IItemRepository mItemRepository;
        private readonly IMapper mMapper;

        public ItemDomain(IItemRepository itemRepository, IMapper mapper)
        {
            mItemRepository = itemRepository;
            mMapper = mapper;
        }


        public async Task<CommandResponse<ItemResponseBase>> CreateAsync(CreateItemRequest createRequest)
        {
            try
            {
                var item = mMapper.Map<Item>(createRequest);
                var itemId = await mItemRepository.CreateAsync(item);
                item.Id = itemId;
                var response = mMapper.Map<ItemResponseBase>(item);

                return CommandResponse<ItemResponseBase>.Succeeded(response);
            }
            catch (Exception ex)
            {
                return CommandResponse<ItemResponseBase>.Failed(ex.Message);
            }
        }

        public async Task<CommandResponse> DeleteAsync(int itemId)
        {
            var item = await mItemRepository.GetByIdAsync(itemId);

            if(item is not null)
            {
                try
                {
                    await mItemRepository.DeleteAsync(itemId);
                    return CommandResponse.Succeeded();
                }
                catch(Exception ex)
                {
                    return CommandResponse.Failed(ex.Message);
                }
            }

            return CommandResponse.Failed($"Item with ID [{itemId}] was not be found");
        }

        public async Task<CommandResponse<IEnumerable<ItemResponseBase>>> GetAllAsync(PaginatedRequest paginatedRequest)
        {
            var items = await mItemRepository.GetAllAsync(paginatedRequest);

            if(items is not null)
            {
                var response = new List<ItemResponseBase>();
                
                foreach(var item in items)
                {
                    response.Add(mMapper.Map<ItemResponseBase>(item));
                }

                return CommandResponse<IEnumerable<ItemResponseBase>>.Succeeded(response);
            }

            return CommandResponse<IEnumerable<ItemResponseBase>>.Failed();
        }

        public async Task<CommandResponse<ItemResponseBase>> GetByIdAsync(int itemId)
        {
            var item = await mItemRepository.GetByIdAsync(itemId);

            if(item is not null)
            {
                var response = new ItemResponseBase();
                mMapper.Map(item, response);

                return CommandResponse<ItemResponseBase>.Succeeded(response);
            }

            return CommandResponse<ItemResponseBase>.Failed();
        }

        public async Task<CommandResponse> UpdateAsync(UpdateItemRequest updateRequest)
        {
            try
            {
                var item = await mItemRepository.GetByIdAsync(updateRequest.Id);

                if(item is not null)
                {
                    mMapper.Map(updateRequest, item);
                    var response = await mItemRepository.UpdateAsync(item);

                    return CommandResponse.Succeeded();
                }
            }
            catch(Exception ex)
            {
                return CommandResponse.Failed(ex.Message);
            }

            return CommandResponse.Failed("Item was not found");
        }
    }
}

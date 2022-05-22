using Task3.Domain.ItemDomain;
using Task3.Model.DTOs;
using Task3.Model.DTOs.Requests;
using Task3.Model.DTOs.Requests.Item;
using Task3.Model.DTOs.Responses.Item;

namespace Task3.Console.Services
{
    public class DbService
    {
        private readonly IItemDomain mDomain;

        public DbService(IItemDomain domain)
        {
            mDomain = domain;
        }


        public async Task<string> GetItemByIdAsync(int id)
        {
            var result = await mDomain.GetByIdAsync(id);

            if(result.Success)
            {
                return $"|..ID..\t|..Name..\n  {result.Result.Id}\t  {result.Result.Name}";
            }
            else
            {
                return "An item was not found";
            }
        }

        public async Task<string> CreateItemAsync(CreateItemRequest request)
        {
            var result = await mDomain.CreateAsync(request);

            if(result.Success)
            {
                return $"An item was successfully added. Id: [{result.Result.Id}]";
            }
            else
            {
                return "An item insertion was failed";
            }
        }

        public async Task<string> UpdateItemAsync(UpdateItemRequest request)
        {
            var result = await mDomain.UpdateAsync(request);

            if(result.Success)
            {
                return $"An item was successfully updated.";
            }
            else
            {
                return $"An item update was failed: {result.Message}";
            }
        }

        public async Task<string> DeleteItemAsync(int id)
        {
            var result = await mDomain.DeleteAsync(id);

            if(result.Success)
            {
                return $"An item was successfully removed.";
            }
            else
            {
                return $"An item removing was failed: {result.Message}";
            }
        }

        public async Task<string> GetAllAsync(PaginatedRequest paginatedReques)
        {
            var result = await mDomain.GetAllAsync(paginatedReques);

            if(result.Success)
            {
                return string.Format("|..ID..\t|..Name..\n{0}",
                    string.Join("\n", result.Result.Select(e => $"  {e.Id}\t  {e.Name}")));
            }
            else
            {
                return "Empty";
            }
        }
    }
}

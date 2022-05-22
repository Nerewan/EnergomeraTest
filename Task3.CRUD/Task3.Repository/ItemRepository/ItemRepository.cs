using Microsoft.EntityFrameworkCore;
using Task3.Model.DTOs.Requests;
using Task3.Model.Entities;

namespace Task3.Repository.ItemRepository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext mContext;


        private ItemRepository()
        {
        }

        public ItemRepository(AppDbContext context)
        {
            mContext = context;
        }


        public async Task<int> CreateAsync(Item record)
        {
            if(record == null)
                throw new ArgumentNullException(nameof(record));

            mContext.Items.Add(record);
            await mContext.SaveChangesAsync();

            return record.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await mContext.Set<Item>().FindAsync(id);
            mContext.Items.Remove(item);
            await mContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetAllAsync(PaginatedRequest paginatedRequest)
        {
            if(paginatedRequest == null)
                throw new ArgumentNullException(nameof(paginatedRequest));

            if(paginatedRequest.IsValid())
            {
                var skipRecordsCount = (paginatedRequest.PageIndex - 1) * paginatedRequest.PageSize;

                return await mContext.Items.Skip(skipRecordsCount.Value)
                    .Take(paginatedRequest.PageSize.Value)
                    .ToListAsync();
            }

            return await mContext.Items.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await mContext.Items.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> UpdateAsync(Item record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            mContext.Items.Update(record);
            await mContext.SaveChangesAsync();

            return record.Id;
        }
    }
}

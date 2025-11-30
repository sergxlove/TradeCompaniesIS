using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly TradeCompanyDbContext _context;

        public ItemsRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Items item, CancellationToken token)
        {
            ItemsEntity itemEntity = new ItemsEntity()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IdWareHouse = item.IdWareHouse,
                Price = item.Price,
                QuantityWareHouse = item.QuantityWareHouse,
            };
            await _context.ItemsTable.AddAsync(itemEntity, token);
            await _context.SaveChangesAsync();
            return itemEntity.Id;
        }

        public async Task<Guid> GetIdByNameAsync(string name, CancellationToken token)
        {
            ItemsEntity? itemEntitu = await _context.ItemsTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == name, token);
            if (itemEntitu is null) return Guid.Empty;
            return itemEntitu.Id;
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _context.ItemsTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<int> UpdatePriceAsync(Guid id, decimal newPrice, CancellationToken token)
        {
            return await _context.ItemsTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(s => s.Price, newPrice), token);
        }

        public async Task<int> UpdateQuantityAsync(Guid id, int quantity, CancellationToken token)
        {
            return await _context.ItemsTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(s => s.QuantityWareHouse, quantity), token);
        }
    }
}

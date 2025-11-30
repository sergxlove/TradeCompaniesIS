using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class SuppliesRepository : ISuppliesRepository
    {
        private readonly TradeCompanyDbContext _context;
        public SuppliesRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Supplies supplies, CancellationToken token)
        {
            SuppliesEntity suppliesEntity = new()
            {
                Id = supplies.Id,
                IdItem = supplies.IdItem,
                IdProvider = supplies.IdProvider,
                IdWarehouse = supplies.IdWarehouse,
                Price = supplies.Price,
                Quantity = supplies.Quantity
            };
            await _context.SuppliesTable.AddAsync(suppliesEntity, token);
            await _context.SaveChangesAsync(token);
            return suppliesEntity.Id;
        }
    }
}

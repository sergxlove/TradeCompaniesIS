using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class WareHousesRepository : IWareHousesRepository
    {
        private readonly TradeCompanyDbContext _context;

        public WareHousesRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(WareHouses wareHouses, CancellationToken token)
        {
            WareHousesEntity wareHousesEntity = new()
            {
                Id = wareHouses.Id,
                IdCountry = wareHouses.IdCountry,
                Address = wareHouses.Address
            };
            await _context.WareHousesTable.AddAsync(wareHousesEntity, token);
            await _context.SaveChangesAsync();
            return wareHousesEntity.Id;
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _context.WareHousesTable
                .AsNoTracking()
                .Where(w => w.Id == id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<List<WareHouses>> GetAllAsync(CancellationToken token)
        {
            List<WareHouses> waresHouses = await _context.WareHousesTable
                .AsNoTracking()
                .Select(a => WareHouses.Create(a.Id, a.Address, a.IdCountry).Value)
                .ToListAsync(token);
            return waresHouses;
        }
    }
}

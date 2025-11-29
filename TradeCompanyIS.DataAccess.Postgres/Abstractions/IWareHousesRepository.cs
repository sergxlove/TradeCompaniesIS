using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface IWareHousesRepository
    {
        Task<Guid> AddAsync(WareHouses wareHouses, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<List<WareHouses>> GetAllAsync(CancellationToken token);
    }
}
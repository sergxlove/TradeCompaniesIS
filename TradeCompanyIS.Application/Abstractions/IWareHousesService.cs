using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface IWareHousesService
    {
        Task<Guid> AddAsync(WareHouses wareHouses, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<List<WareHouses>> GetAllAsync(CancellationToken token);
    }
}
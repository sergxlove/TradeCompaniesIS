using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface IProvidersRepository
    {
        Task<Guid> AddAsync(Providers provider, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<List<Providers>> GetAllAsync();
    }
}
using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface IProvidersService
    {
        Task<Guid> AddAsync(Providers provider, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<List<Providers>> GetAllAsync();
    }
}
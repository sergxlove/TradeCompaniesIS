using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface ICountriesService
    {
        Task<Guid> AddAsync(Countries country, CancellationToken token);
        Task<int> DeleteAsync(string name, CancellationToken token);
        Task<Guid> GetIdByName(string name, CancellationToken token);
    }
}
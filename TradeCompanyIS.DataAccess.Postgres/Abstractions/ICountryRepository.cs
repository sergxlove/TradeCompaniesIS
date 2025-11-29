using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface ICountryRepository
    {
        Task<Guid> AddAsync(Countries country, CancellationToken token);
        Task<int> DeleteAsync(string name, CancellationToken token);
        Task<Guid> GetIdByName(string name, CancellationToken token);
    }
}
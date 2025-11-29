using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface IClientsRepository
    {
        Task<Guid> AddAsync(Clients client, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<Clients?> GetAsync(Guid id, CancellationToken token);
        Task<Guid> GetIdByEmail(string email, CancellationToken token);
    }
}
using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface IClientsService
    {
        Task<Guid> AddAsync(Clients client, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<Clients?> GetAsync(Guid id, CancellationToken token);
        Task<Guid> GetIdByEmail(string email, CancellationToken token);
    }
}
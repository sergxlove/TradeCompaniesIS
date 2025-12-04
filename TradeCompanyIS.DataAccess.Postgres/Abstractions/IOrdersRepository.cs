using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface IOrdersRepository
    {
        Task<Guid> AddAsync(Orders order, CancellationToken token);
        Task<List<Orders>> GetByIdClientAsync(Guid idClient, CancellationToken token);
    }
}
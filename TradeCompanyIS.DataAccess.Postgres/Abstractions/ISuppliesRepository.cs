using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface ISuppliesRepository
    {
        Task<Guid> AddAsync(Supplies supplies, CancellationToken token);
    }
}
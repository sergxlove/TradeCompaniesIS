using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface ISuppliesService
    {
        Task<Guid> AddAsync(Supplies supplies, CancellationToken token);
    }
}
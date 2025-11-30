using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface IOrdersService
    {
        Task<Guid> AddAsync(Orders order, CancellationToken token);
    }
}
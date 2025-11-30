using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface IItemsRepository
    {
        Task<Guid> AddAsync(Items item, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<Guid> GetIdByNameAsync(string name, CancellationToken token);
        Task<int> UpdatePriceAsync(Guid id, decimal newPrice, CancellationToken token);
        Task<int> UpdateQuantityAsync(Guid id, int quantity, CancellationToken token);
    }
}
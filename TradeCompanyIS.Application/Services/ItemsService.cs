using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.Application.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IItemsRepository _repository;
        public ItemsService(IItemsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAsync(Items item, CancellationToken token)
        {
            return await _repository.AddAsync(item, token);
        }
        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }
        public async Task<Guid> GetIdByNameAsync(string name, CancellationToken token)
        {
            return await _repository.GetIdByNameAsync(name, token);
        }
        public async Task<int> UpdatePriceAsync(Guid id, decimal newPrice, CancellationToken token)
        {
            return await _repository.UpdatePriceAsync(id, newPrice, token);
        }
        public async Task<int> UpdateQuantityAsync(Guid id, int quantity, CancellationToken token)
        {
            return await _repository.UpdateQuantityAsync(id, quantity, token);
        }
    }
}

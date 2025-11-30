using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.Application.Services
{
    public class WareHousesService : IWareHousesService
    {
        private readonly IWareHousesRepository _repository;
        public WareHousesService(IWareHousesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAsync(WareHouses wareHouses, CancellationToken token)
        {
            return await _repository.AddAsync(wareHouses, token);
        }
        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }
        public async Task<List<WareHouses>> GetAllAsync(CancellationToken token)
        {
            return await _repository.GetAllAsync(token);
        }
    }
}

using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.Application.Services
{
    public class ProvidersService : IProvidersService
    {
        private readonly IProvidersRepository _repository;
        public ProvidersService(IProvidersRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAsync(Providers provider, CancellationToken token)
        {
            return await _repository.AddAsync(provider, token);
        }
        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }
        public async Task<List<Providers>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}

using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.Application.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _repository;
        public ClientsService(IClientsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAsync(Clients client, CancellationToken token)
        {
            return await _repository.AddAsync(client, token);
        }
        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }
        public async Task<Clients?> GetAsync(Guid id, CancellationToken token)
        {
            return await _repository.GetAsync(id, token);
        }
        public async Task<Guid> GetIdByEmailAsync(string email, CancellationToken token)
        {
            return await _repository.GetIdByEmail(email, token);
        }
        public async Task<bool> CheckAsync(string email, CancellationToken token)
        {
            return await _repository.CheckAsync(email, token);
        }
    }
}

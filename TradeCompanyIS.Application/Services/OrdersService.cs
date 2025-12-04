using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repository;
        public OrdersService(IOrdersRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAsync(Orders order, CancellationToken token)
        {
            return await _repository.AddAsync(order, token);
        }

        public async Task<List<Orders>> GetByIdClientAsync(Guid idClient, CancellationToken token)
        {
            return await _repository.GetByIdClientAsync(idClient, token);
        }
    }
}

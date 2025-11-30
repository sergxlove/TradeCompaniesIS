using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.Application.Services
{
    public class SuppliesService : ISuppliesService
    {
        private readonly ISuppliesRepository _repository;
        public SuppliesService(ISuppliesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAsync(Supplies supplies, CancellationToken token)
        {
            return await _repository.AddAsync(supplies, token);
        }
    }
}

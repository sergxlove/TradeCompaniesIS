using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.Application.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountryRepository _repository;
        public CountriesService(ICountryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAsync(Countries country, CancellationToken token)
        {
            return await _repository.AddAsync(country, token);
        }
        public async Task<int> DeleteAsync(string name, CancellationToken token)
        {
            return await _repository.DeleteAsync(name, token);
        }
        public async Task<Guid> GetIdByName(string name, CancellationToken token)
        {
            return await _repository.GetIdByName(name, token);
        }

    }
}

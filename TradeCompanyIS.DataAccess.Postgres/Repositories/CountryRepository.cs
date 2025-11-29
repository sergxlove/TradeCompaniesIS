using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly TradeCompanyDbContext _context;

        public CountryRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Countries country, CancellationToken token)
        {
            CountriesEntity countryEntity = new CountriesEntity()
            {
                Id = country.Id,
                Name = country.Name
            };
            await _context.CountriesTable.AddAsync(countryEntity, token);
            await _context.SaveChangesAsync(token);
            return countryEntity.Id;
        }

        public async Task<int> DeleteAsync(string name, CancellationToken token)
        {
            return await _context.CountriesTable
                .AsNoTracking()
                .Where(country => country.Name == name)
                .ExecuteDeleteAsync(token);
        }

        public async Task<Guid> GetIdByName(string name, CancellationToken token)
        {
            CountriesEntity? countryEntity = await _context.CountriesTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == name, token);
            if (countryEntity is null) return Guid.Empty;
            return countryEntity.Id;
        }
    }
}

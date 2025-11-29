using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class ProvidersRepository : IProvidersRepository
    {
        private readonly TradeCompanyDbContext _context;

        public ProvidersRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Providers provider, CancellationToken token)
        {
            ProvidersEntity providersEntity = new ProvidersEntity()
            {
                Id = provider.Id,
                Name = provider.Name,
                Address = provider.Address,
                IdCountry = provider.IdCountry,
                NumberPhone = provider.NumberPhone,
            };
            await _context.ProvidersTable.AddAsync(providersEntity, token);
            await _context.SaveChangesAsync();
            return provider.Id;
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _context.ProvidersTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<List<Providers>> GetAllAsync()
        {
            return await _context.ProvidersTable
                .AsNoTracking()
                .Select(a => Providers.Create(a.Id, a.Name, a.NumberPhone, a.IdCountry, a.Address).Value)
                .ToListAsync();
        }
    }
}

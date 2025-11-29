using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly TradeCompanyDbContext _context;

        public ClientsRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Clients client, CancellationToken token)
        {
            ClientsEntity clientEntity = new ClientsEntity()
            {
                Id = client.Id,
                Email = client.Email,
                AddressDelivery = client.AddressDelivery,
                IdCountry = client.IdCountry,
                NameClient = client.NameClient,
                NumberPhone = client.NumberPhone,
            };
            await _context.ClientsTable.AddAsync(clientEntity, token);
            await _context.SaveChangesAsync();
            return clientEntity.Id;
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _context.ClientsTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<Guid> GetIdByEmail(string email, CancellationToken token)
        {
            ClientsEntity? clientsEntity = await _context.ClientsTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email == email, token);
            if (clientsEntity is null) return Guid.Empty;
            return clientsEntity.Id;
        }

        public async Task<Clients?> GetAsync(Guid id, CancellationToken token)
        {
            ClientsEntity? clientEntity = await _context.ClientsTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, token);
            if (clientEntity is null) return null;
            return Clients.Create(clientEntity.Id, clientEntity.NameClient, clientEntity.NumberPhone,
                clientEntity.Email, clientEntity.IdCountry, clientEntity.AddressDelivery).Value;
        }
    }
}

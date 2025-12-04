using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly TradeCompanyDbContext _context;
        public OrdersRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Orders order, CancellationToken token)
        {
            OrdersEntity orderEntity = new()
            {
                Id = order.Id,
                DateReg = order.DateReg,
                IdClients = order.IdClients,
                IdItem = order.IdItem,
                Quantity = order.Quantity,
            };
            await _context.OredersTable.AddAsync(orderEntity, token);
            await _context.SaveChangesAsync();
            return orderEntity.Id;
        }

        public async Task<List<Orders>> GetByIdClientAsync(Guid idClient, CancellationToken token)
        {
            List<Orders> ordersEntity = new();
            ordersEntity = await _context.OredersTable
                .AsNoTracking()
                .Where(a => a.IdClients == idClient)
                .Select(a => Orders.Create(a.Id, a.IdClients, a.IdItem, a.DateReg, a.Quantity).Value)
                .ToListAsync(token);
            return ordersEntity;
        }

    }
}

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

    }
}

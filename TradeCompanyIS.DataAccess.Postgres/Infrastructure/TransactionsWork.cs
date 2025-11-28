using Microsoft.EntityFrameworkCore.Storage;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;

namespace TradeCompanyIS.DataAccess.Postgres.Infrastructure
{
    public class TransactionsWork : ITransactionsWork
    {
        private readonly TradeCompanyDbContext _context;
        private IDbContextTransaction? _transaction;

        public TransactionsWork(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction!.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction!.RollbackAsync();
        }
    }
}

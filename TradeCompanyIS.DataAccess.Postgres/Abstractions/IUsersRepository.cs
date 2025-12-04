using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> CreateAsync(Users user, CancellationToken token);
        Task<bool> VerifyAsync(string username, string password, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
    }
}
using TradeCompanyIS.Core.Models;

namespace TradeCompanyIS.Application.Abstractions
{
    public interface IUsersService
    {
        Task<Guid> CreateAsync(Users user, CancellationToken token);
        Task<bool> VerifyAsync(string username, string password, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
    }
}
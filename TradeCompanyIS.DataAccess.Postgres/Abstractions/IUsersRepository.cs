using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Response;

namespace TradeCompanyIS.DataAccess.Postgres.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> CreateAsync(Users user, CancellationToken token);
        Task<bool> VerifyAsync(string username, string password, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<bool> CheckAsync(string username, CancellationToken token);
        Task<int> UpdatePasswordAsync(string username, string newPassword, CancellationToken token);
        Task<string> GetRoleAsync(string username, CancellationToken token);
        Task<Guid> GetIdByUsernameAsync(string username, CancellationToken token);
        Task<List<UsersResponse>> GetAllUsersAsync(CancellationToken token);
    }
}
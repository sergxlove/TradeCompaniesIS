using TradeCompanyIS.Application.Abstractions;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Response;

namespace TradeCompanyIS.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> CreateAsync(Users user, CancellationToken token)
        {
            return await _repository.CreateAsync(user, token);
        }
        public async Task<bool> VerifyAsync(string username, string password, CancellationToken token)
        {
            return await _repository.VerifyAsync(username, password, token);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }

        public async Task<bool> CheckAsync(string username, CancellationToken token)
        {
            return await _repository.CheckAsync(username, token);
        }
        public async Task<int> UpdatePasswordAsync(string username, string newPassword, 
            CancellationToken token)
        {
            return await _repository.UpdatePasswordAsync(username, newPassword, token);
        }

        public async Task<string> GetRoleAsync(string username, CancellationToken token)
        {
            return await _repository.GetRoleAsync(username, token);
        }

        public async Task<Guid> GetIdByUsernameAsync(string username, CancellationToken token)
        {
            return await _repository.GetIdByUsernameAsync(username, token);
        }

        public async Task<List<UsersResponse>> GetAllUsersAsync(CancellationToken token)
        {
            return await _repository.GetAllUsersAsync(token);
        }
    }
}

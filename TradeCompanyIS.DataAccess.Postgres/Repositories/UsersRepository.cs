using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;
using TradeCompanyIS.DataAccess.Postgres.Response;

namespace TradeCompanyIS.DataAccess.Postgres.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TradeCompanyDbContext _context;

        public UsersRepository(TradeCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Users user, CancellationToken token)
        {
            try
            {
                UsersEntity usersEntity = new UsersEntity()
                {
                    Id = user.Id,
                    Username = user.Username,
                    HashPassword = user.HashPassword,
                    Role = user.Role
                };
                await _context.UsersTable.AddAsync(usersEntity, token);
                await _context.SaveChangesAsync(token);
                return usersEntity.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> CheckAsync(string username, CancellationToken token)
        {
            UsersEntity? result = await _context.UsersTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Username == username, token);
            if (result is null) return false;
            return true;
        }

        public async Task<int> UpdatePasswordAsync(string username, string newPassword,  
            CancellationToken token)
        {
            return await _context.UsersTable
                .AsNoTracking()
                .Where(a => a.Username == username)
                .ExecuteUpdateAsync(a => a.SetProperty(a => a.HashPassword, newPassword), token);
        }

        public async Task<string> GetRoleAsync(string username, CancellationToken token)
        {
            UsersEntity? result = await _context.UsersTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Username == username, token);
            if (result is null) return string.Empty;
            return result.Role;
        }

        public async Task<Guid> GetIdByUsernameAsync(string username, CancellationToken token)
        {
            UsersEntity? result = await _context.UsersTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Username == username, token);
            if (result is null) return Guid.Empty;
            return result.Id;
        }

        public async Task<bool> VerifyAsync(string username, string password, CancellationToken token)
        {
            var user = await _context.UsersTable.FirstOrDefaultAsync(a => a.Username == username, token);
            if (user == null) return false;
            return Users.VerifyPassword(password, user.HashPassword);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _context.UsersTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<List<UsersResponse>> GetAllUsersAsync(CancellationToken token)
        {
            List<UsersEntity> resultEntity = await _context.UsersTable
                .AsNoTracking()
                .ToListAsync(token);
            List<UsersResponse> result = new List<UsersResponse>();
            foreach(UsersEntity user in resultEntity)
            {
                result.Add(new UsersResponse { Role = user.Role, Username = user.Username });
            }
            return result;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.Core.Models;
using TradeCompanyIS.DataAccess.Postgres.Abstractions;
using TradeCompanyIS.DataAccess.Postgres.Models;

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

        public async Task<bool> VerifyAsync(string username, string password, CancellationToken token)
        {
            var user = await _context.UsersTable.FirstOrDefaultAsync(a => a.Username == username, token);
            if (user == null) return false;
            return Users.VerifyPassword(password, user.HashPassword);
        }
    }
}

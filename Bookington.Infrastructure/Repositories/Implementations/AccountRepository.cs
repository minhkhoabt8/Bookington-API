using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class AccountRepository : GenericRepository<Account, BookingtonDbContext>, IAccountRepository
    {
        public AccountRepository(BookingtonDbContext context) : base(context)
        {
        }

        public async Task<Account?> FindAccountByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == phoneNumber && a.IsDeleted == false);
        }
        public async Task<Account?> LoginByPhoneAsync(AccountLoginInputDTO login)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(c => c.Phone == login.Phone);

            bool verified = BCrypt.Net.BCrypt.Verify(login.Password, account.Password);

            if (verified == true)
            {
                return account;
            }

            return null;

        }

        public async Task<IEnumerable<Account>> QueryAsync(AccountQuery query, bool trackChanges = false)
        {
            IQueryable<Account> accounts = _context.Accounts.Include(a => a.Role).Where(a => a.IsDeleted == false);
            if (!trackChanges)
            {
                accounts = accounts.AsNoTracking();
            }
            if (!query.SearchField.IsNullOrEmpty() )
            {
                accounts = accounts.Where(c => c.FullName.Contains(query.SearchField) || c.Phone.Contains(query.SearchField));
            }
            //if( query.Role != null)
            //{
            //    accounts = accounts.Where(a => a.RoleId == query.Role.ToString());
            //}

            return accounts;
        }
    }
}

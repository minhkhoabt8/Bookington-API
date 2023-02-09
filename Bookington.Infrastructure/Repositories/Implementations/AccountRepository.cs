using Azure;
using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.Court;
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
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == phoneNumber);
        }
        public async Task<Account?> LoginByPhoneAsync(AccountLoginInputDTO login)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == login.Phone && a.Password == login.Password);
        }

        public async Task<IEnumerable<Account>> QueryAsync(AccountQuery query, bool trackChanges = false)
        {
            IQueryable<Account> accounts = _context.Accounts;
            if (!query.SearchField.IsNullOrEmpty())
            {
                accounts = accounts.Where(c => c.FullName.Contains(query.SearchField) || c.Phone.Contains(query.SearchField));
            }


            return accounts;
        }
    }
}

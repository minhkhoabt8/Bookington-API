using Azure;
using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == login.Phone && a.Password==login.Password);
        }

    }
}

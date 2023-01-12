using Azure;
using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedLib.Infrastructure.Repositories.Implementations;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class AccountRepository : GenericRepository<Account, BookingtonDbContext>, IAccountRepository
    {
        public AccountRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<Account?> FindAccountByPhoneNumber(string phoneNumber)
        {
            return _context.Accounts.FirstOrDefaultAsync(a => a.Phone == phoneNumber);
        }
        public Task<Account?> LoginByPhone(AccountLoginInputDTO login)
        {
            return  _context.Accounts.FirstOrDefaultAsync(a => a.Phone == login.Phone && a.Password==login.Password);
        }

    }
}

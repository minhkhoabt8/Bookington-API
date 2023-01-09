using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using SharedLib.Infrastructure.Repositories.Implementations;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class AccountRepository : GenericRepository<Account, BookingtonDbContext>, IAccountRepository
    {
        public AccountRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

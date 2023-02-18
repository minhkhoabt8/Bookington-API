using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class UserBalanceRepository : GenericRepository<UserBalance, BookingtonDbContext>, IUserBalanceRepository
    {
        public UserBalanceRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<UserBalance?> FindByAccountIdAsync(string accountId, bool trackChanges = false)
        {
            IQueryable<UserBalance> dbSet = _context.Set<UserBalance>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return Task.FromResult(dbSet.FirstOrDefault(ub => ub.RefUser == accountId));
        }
    }
}

using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Core.Enums;
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

        public async Task<UserBalance?> FindAdminAccountBalance()
        {
            return await _context.UserBalances
                .Include(b => b.RefUserNavigation)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(b => b.RefUserNavigation.Role.RoleName == AccountRole.admin.ToString());
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

        public async Task<IEnumerable<UserBalance?>> GetUserBalancesOfOwnerToPayout()
        {
            double minMoney = 20000;

            return await _context.UserBalances
               .Include(b => b.RefUserNavigation)
               .ThenInclude(u => u.Role)
               .Where(b => b.RefUserNavigation.Role.RoleName == AccountRole.owner.ToString() && b.Balance >= minMoney)
               .ToListAsync();
        }
    }
}

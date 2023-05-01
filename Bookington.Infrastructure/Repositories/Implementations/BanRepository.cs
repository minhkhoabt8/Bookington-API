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
    public class BanRepository : GenericRepository<Ban, BookingtonDbContext>, IBanRepository
    {
        public BanRepository(BookingtonDbContext context) : base(context)
        {
        }

        public async Task<Ban?> FindCourtBanByCourtIdAsync(string courtId)
        {
            return await _context.Bans.FirstOrDefaultAsync(c => c.RefCourt == courtId && c.BanUntil > DateTime.Now && c.IsCourtBan && c.IsActive);
        }

        public async Task<Ban?> FindUserBanByUserIdAsync(string userId)
        {
            return await _context.Bans.FirstOrDefaultAsync(c => c.RefAccount == userId && c.BanUntil > DateTime.Now && c.IsAccountBan && c.IsActive);
        }

        public async Task<IEnumerable<Ban?>> GetAllExpiredCourtBanAsync()
        {
            return  await _context.Bans
                    .Where(b => b.IsCourtBan && b.IsActive && b.BanUntil < DateTime.Now)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Ban?>> GetAllExpiredUserBanAsync()
        {
            return await _context.Bans
                    .Where(b => b.IsAccountBan && b.IsActive && b.BanUntil < DateTime.Now)
                    .ToListAsync();
        }
    }
}

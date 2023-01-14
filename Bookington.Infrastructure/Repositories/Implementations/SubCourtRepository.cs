using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Core.Data;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class SubCourtRepository : GenericRepository<SubCourt, BookingtonDbContext>, ISubCourtRepository
    {
        public SubCourtRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<SubCourt>> GetAvailableSubCourtsByCourtId(string courtId, bool trackChanges = false)
        {
            IQueryable<SubCourt> dbSet = _context.Set<SubCourt>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return Task.FromResult(dbSet.Include(sc => sc.CourtType).Where(sc => sc.IsActive == true && sc.IsDeleted == false).AsEnumerable());
        }
    }
}

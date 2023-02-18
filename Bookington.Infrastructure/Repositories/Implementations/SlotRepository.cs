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
    public class SlotRepository : GenericRepository<Slot, BookingtonDbContext>, ISlotRepository
    {
        public SlotRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<Account> GetCourtOwnerBySlotId(string slotId, bool trackChanges = false)
        {
            IQueryable<Slot> dbSet = _context.Set<Slot>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var slot = dbSet.Include(s => s.RefSubCourtNavigation).
                Include(s => s.RefSubCourtNavigation.ParentCourt).
                Include(s => s.RefSubCourtNavigation.ParentCourt.Owner).
                FirstOrDefault(s => s.Id == slotId);

            return Task.FromResult(slot?.RefSubCourtNavigation.ParentCourt.Owner!);
        }

        public Task<string> GetCourtNameBySlotId(string slotId, bool trackChanges = false)
        {
            IQueryable<Slot> dbSet = _context.Set<Slot>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var slot = dbSet.Include(s => s.RefSubCourtNavigation).
                Include(s => s.RefSubCourtNavigation.ParentCourt).                
                FirstOrDefault(s => s.Id == slotId);

            return Task.FromResult(slot?.RefSubCourtNavigation.ParentCourt.Name!);
        }
    }
}

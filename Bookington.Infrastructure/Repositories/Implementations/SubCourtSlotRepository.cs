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
    public class SubCourtSlotRepository : GenericRepository<SubCourtSlot, BookingtonDbContext>, ISubCourtSlotRepository
    {
        public SubCourtSlotRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<bool> SubCourtHasSlot(string subCourtId)
        {
            var scs = _context.SubCourtSlots.FirstOrDefault(scs => scs.RefSubCourt == subCourtId);

            if (scs == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task FlushSlotsOfSubCourt(string subCourtId)
        {
            _context.SubCourtSlots.ExecuteDelete();  
            
            return Task.CompletedTask;
        }

        public Task<SubCourtSlot> FindAsync(string subCourtId, string slotId, bool trackChanges = true)
        {
            IQueryable<SubCourtSlot> subCourtSlots = _context.Set<SubCourtSlot>();

            if (!trackChanges)
            {
                subCourtSlots = subCourtSlots.AsNoTracking();
            }

            return subCourtSlots.FirstOrDefaultAsync(scs => scs.RefSubCourt == subCourtId && scs.RefSlot == slotId)!;
        }

        public Task<IEnumerable<SubCourtSlot>> GetScheduleOfASubCourt(string subCourtId)
        {
            var slots = _context.SubCourtSlots.Include(scs => scs.RefSlotNavigation).ToList();

            var result = new List<SubCourtSlot>();

            // DaysOfWeek is a list of string that represents 7 days in a week starting from Monday -> Sunday
            List<string> daysOfWeek = new List<string>();

            foreach (var day in DayOfWeek.GetValues(typeof(DayOfWeek)))
            {
                daysOfWeek.Add(day.ToString()!);
            }

            var sunday = daysOfWeek.ElementAt(0);
            daysOfWeek.RemoveAt(0);
            daysOfWeek.Add(sunday);

            // Sort from Monday -> Sunday and ascendingly by startTime
            var currDaySlots = new List<SubCourtSlot>();

            foreach (var day in daysOfWeek)
            {
                currDaySlots.Clear();

                currDaySlots = slots.Where(s => s.RefSlotNavigation.DaysInSchedule == day).ToList();

                result.AddRange(currDaySlots.OrderBy(s => s.RefSlotNavigation.StartTime));
            }

            return Task.FromResult(result.AsEnumerable());
        }

        public async Task<IEnumerable<SubCourtSlot?>> GetListSubCourtSlotsBySubCourtId(string subCourtId)
        {
            return await Task.FromResult(_context.SubCourtSlots.Where(c => c.RefSubCourt == subCourtId).AsEnumerable());
        }
    }
}

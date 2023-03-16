using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Core.Data;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.DTOs.Slot;

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

        public Task<IEnumerable<Slot>> GetAvailableSlotsForBooking(SlotQueryForBooking dto, bool trackChanges = false)
        {
            IQueryable<Slot> slots = _context.Set<Slot>();            

            if (trackChanges == false)
            {
                slots = slots.AsNoTracking();                
            }

            var activeSlots = slots.Where(s => s.IsActive && s.RefSubCourt == dto.SubCourtId).ToList();
            var atvSlots = activeSlots.Select(s => s.Id).ToList();

            var dotw = dto.PlayDate.DayOfWeek;

            var bookings = _context.Bookings.Include(b => b.RefOrderNavigation).ToList();

            var bookingsOnPlayDate = bookings.Where(b => b.PlayDate.DayOfWeek == dotw &&
                                                         b.RefOrderNavigation.IsPaid && 
                                                        !b.RefOrderNavigation.IsRefunded && 
                                                        !b.RefOrderNavigation.IsCanceled &&
                                                         atvSlots.Contains(b.RefSlot))
                                             .ToList();

            var slotsBookedOnPlayDate = bookingsOnPlayDate.Select(b => b.RefSlot).ToList();

            foreach (var slot in activeSlots)
            {
                if (slotsBookedOnPlayDate.Contains(slot.Id)) slot.IsActive = false;
            }

            return Task.FromResult(activeSlots.OrderBy(s => s.StartTime).AsEnumerable());
        }

        public Task<bool> IsSlotBooked(string slotId, DateTime playDate, bool trackChanges = false)
        {
            var bookings = _context.Bookings.Include(b => b.RefOrderNavigation).ToList();

            var foundSlot = bookings.FirstOrDefault(b => b.RefSlot == slotId
                                                      && b.RefOrderNavigation.IsPaid && !b.RefOrderNavigation.IsRefunded & !b.RefOrderNavigation.IsCanceled
                                                      && playDate.CompareTo(playDate) == 0);
            if (foundSlot == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}

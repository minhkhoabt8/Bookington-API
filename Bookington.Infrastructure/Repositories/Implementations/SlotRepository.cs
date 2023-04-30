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
using Microsoft.IdentityModel.Tokens;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class SlotRepository : GenericRepository<Slot, BookingtonDbContext>, ISlotRepository
    {
        public SlotRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Slot>> GetAllDefaultSlotsAsync()
        {
            var slots = _context.Slots.ToList();

            var result = new List<Slot>();

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
            var currDaySlots = new List<Slot>();

            foreach (var day in daysOfWeek)
            {
                currDaySlots = slots.Where(s => s.DaysInSchedule == day).ToList();

                result.AddRange(currDaySlots.OrderBy(s => s.StartTime));
            }

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<SubCourtSlot>> GetAvailableSlotsForBooking(SlotQueryForBooking dto)
        {
            var dotw = dto.PlayDate.DayOfWeek.ToString();

            // Get all active slots of a sub court
            // And it has to be on the DayOfWeek of Play Date
            var activeSCSlots = _context.SubCourtSlots.Include(scs => scs.RefSlotNavigation)
                                                      .Where(scs => scs.IsActive && scs.RefSubCourt == dto.SubCourtId
                                                                 && scs.RefSlotNavigation.DaysInSchedule == dotw);

            // Get all booked slots via Bookings
            // Bookings must be paid and not refunded or canceled
            // Only check for bookings on Play Date
            var bookedSlotsOnPlayDate = _context.Bookings.Include(b => b.RefOrderNavigation).ToList()
                                                      .Where(b => b.RefSubCourt == dto.SubCourtId
                                                              &&  b.RefOrderNavigation.IsPaid
                                                              && !b.RefOrderNavigation.IsRefunded
                                                              && !b.RefOrderNavigation.IsCanceled
                                                              &&  b.PlayDate.CompareTo(dto.PlayDate.ToDateTime(TimeOnly.MinValue)) == 0)
                                                      .Select(b => b.RefSlot).ToList();

            // Set status IsActive of sub court slot to false if slot is booked
            // This field will be used for mapping to SlotForBookingReadDTO
            foreach (var slot in activeSCSlots)
            {
                // Slot before current time will be marked as false as well
                // Check if slot is before current time of today
                var slotStartTime = new DateTime(dto.PlayDate.Year, dto.PlayDate.Month, dto.PlayDate.Day, slot.RefSlotNavigation.StartTime.Hours, slot.RefSlotNavigation.StartTime.Minutes, slot.RefSlotNavigation.StartTime.Seconds);
                var currentDateTime = DateTime.Now;
                var currentTimeOfDay = currentDateTime.TimeOfDay;
                if (slotStartTime.Date == currentDateTime.Date && slotStartTime.TimeOfDay.CompareTo(currentTimeOfDay) <= 0)
                {
                    slot.IsActive = false;
                }
                else
                {
                    foreach (var bookedSlot in bookedSlotsOnPlayDate)
                    {
                        if (slot.RefSlot == bookedSlot)
                        {
                            slot.IsActive = false;
                        }
                    }
                }
            }

            return Task.FromResult(activeSCSlots.OrderBy(s => s.RefSlotNavigation.StartTime).AsEnumerable());            
        }

        public Task<bool> IsSlotBooked(string slotId, DateTime playDate)
        {
            var bookings = _context.Bookings.Include(b => b.RefOrderNavigation).ToList();

            var foundSlot = bookings.FirstOrDefault(b => b.RefSlot == slotId
                                                      && b.RefOrderNavigation.IsPaid && !b.RefOrderNavigation.IsRefunded & !b.RefOrderNavigation.IsCanceled
                                                      && playDate.CompareTo(playDate) == 0 && !b.IsCancel);

            if (foundSlot == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task<double> GetTheLowestSlotPriceOfACourt(string courtId, bool trackChanges = false)
        {
            IQueryable<SubCourtSlot> dbSet = _context.Set<SubCourtSlot>().Include(sc => sc.RefSubCourtNavigation)
                                                                         .Include(sc => sc.RefSubCourtNavigation.ParentCourt);
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var slotsOfCourt = dbSet.Where(s => s.IsActive && s.RefSubCourtNavigation.ParentCourtId == courtId).ToList();

            if (slotsOfCourt.IsNullOrEmpty()) return Task.FromResult((double) 0);

            return Task.FromResult(slotsOfCourt.Min(s => s.Price));
        }
    }
}

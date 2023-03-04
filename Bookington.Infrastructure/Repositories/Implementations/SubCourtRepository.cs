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

        public Task<IEnumerable<SubCourt>> GetSubCourtsForBooking(SubCourtQueryForBooking dto, bool trackChanges = false)
        {
            IQueryable<SubCourt> subCourts = _context.Set<SubCourt>();
            IQueryable<Slot> slots = _context.Set<Slot>();
            if (trackChanges == false)
            {
                subCourts = subCourts.AsNoTracking();
                slots = slots.AsNoTracking();
            }

            var dotw = dto.PlayDate.DayOfWeek.ToString();
            var startTime = dto.StartTime.ToTimeSpan();
            var endTime = dto.EndTime.ToTimeSpan();

            // Get available slots that fits with time frame (between StartTime and EndTime)
            var activeSubCourts = subCourts.Where(sc => sc.IsActive && sc.ParentCourtId == dto.CourtId).ToList();

            var avSlots = slots.Where(s => (s.IsActive && s.DaysInSchedule == dotw &&
                                            activeSubCourts.Select(sc => sc.Id).Contains(s.RefSubCourt)) &&                                                                                   
                                            s.StartTime.CompareTo(startTime) >= 0 &&
                                            s.EndTime.CompareTo(endTime) <= 0)
                               .ToList();            

            var bookings = _context.Bookings.Include(b => b.RefOrderNavigation).Include(b => b.RefSlotNavigation).ToList();

            var bookingsOnPlayDate = bookings.Where(b => b.PlayDate.DayOfWeek.ToString() == dotw &&
                                                         b.RefOrderNavigation.IsPaid &&
                                                        !b.RefOrderNavigation.IsRefunded &&
                                                        !b.RefOrderNavigation.IsCanceled &&
                                                         avSlots.Select(s => s.Id).Contains(b.RefSlot))
                                             .ToList();

            // Get booked slots             
            var bookedSlots = bookingsOnPlayDate.Select(b => b.RefSlotNavigation).ToList();

            // Remove booked slots from available slots
            foreach (var bs in bookedSlots)
            {
                var findSlot = avSlots.Find(avs => avs.Id == bs.Id);
                avSlots.Remove(findSlot);
            }

            // Get sub court part from slots to set status later
            // avscs = available sub courts
            var avscs = avSlots.Select(s => s.RefSubCourt).ToList();

            var hashSetOfavscs = new HashSet<string>(avscs);

            // For better understanding result 
            var resultSubCourts = activeSubCourts;
            
            // Set IsActive status to map with its available status
            foreach (var sc in resultSubCourts)
            {
                if (!hashSetOfavscs.Contains(sc.Id)) sc.IsActive = false;                
            }

            return Task.FromResult(resultSubCourts.AsEnumerable());
        }

        /*public Task<IEnumerable<SubCourt>> GetUnavailableSubCourtsForBooking(SubCourtQueryForBooking dto, bool trackChanges = false)
        {
            IQueryable<SubCourt> subCourts = _context.Set<SubCourt>();
            IQueryable<Slot> slots = _context.Set<Slot>();
            if (trackChanges == false)
            {
                subCourts = subCourts.AsNoTracking();
                slots = slots.AsNoTracking();
            }

            var dotw = dto.PlayDate.DayOfWeek.ToString();
            var startTime = dto.StartTime.ToTimeSpan();
            var endTime = dto.EndTime.ToTimeSpan();

            var activeSubCourts = subCourts.Where(sc => sc.IsActive && sc.ParentCourtId == dto.CourtId).ToList();

            var avSlots = slots.Where(s => s.IsActive && s.DaysInSchedule == dotw
                                        && activeSubCourts.Select(sc => sc.Id).Contains(s.RefSubCourt)
                                        && s.StartTime.CompareTo(startTime) >= 0
                                        && s.EndTime.CompareTo(endTime) <= 0).ToList();

            var avscs = avSlots.Select(s => s.RefSubCourt).ToList();

            var unavSubCourts = activeSubCourts;

            // Remove all available slots then what's left is the unavailable ones
            foreach (var scId in avscs)
            {
                var currSc = unavSubCourts.FirstOrDefault(sc => sc.Id == scId);
                if (currSc != null) unavSubCourts.Remove(currSc);
            }

            return Task.FromResult(unavSubCourts.AsEnumerable());
        }*/
    }
}

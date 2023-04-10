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
using System.Xml.Serialization;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class SubCourtRepository : GenericRepository<SubCourt, BookingtonDbContext>, ISubCourtRepository
    {
        public SubCourtRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<int> GetNumberOfSubCourts(string courtId, bool trackChanges = false) 
        {
            IQueryable<SubCourt> dbSet = _context.Set<SubCourt>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return Task.FromResult(dbSet.Where(sc => sc.ParentCourtId == courtId).Count());
        }

        public Task<IEnumerable<SubCourt>> GetAvailableSubCourtsByCourtId(string courtId, bool trackChanges = false)
        {
            IQueryable<SubCourt> dbSet = _context.Set<SubCourt>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return Task.FromResult(dbSet.Include(sc => sc.CourtType).Where(sc => sc.IsActive == true && sc.IsDeleted == false && sc.ParentCourtId == courtId).AsEnumerable());
        }

        public Task<IEnumerable<SubCourt>> GetSubCourtsForBooking(SubCourtQueryForBooking dto)
        {           
            var dotw = dto.PlayDate.DayOfWeek.ToString();
            var startTime = dto.StartTime.ToTimeSpan();
            var endTime = dto.EndTime.ToTimeSpan();

            // Get all active sub courts of court
            var atvSubCourts = _context.SubCourts.Where(sc => sc.IsActive && sc.ParentCourtId == dto.CourtId).ToList();

            // Get all active slots of the sub courts above
            var atvSlotsOfSubCourts = _context.SubCourtSlots.Include(scs => scs.RefSlotNavigation)
                                                            .Where(scs => scs.IsActive 
                                                                       && scs.RefSlotNavigation.DaysInSchedule == dotw
                                                                       && atvSubCourts.Select(sc => sc.Id).Contains(scs.RefSubCourt))                                                                       
                                                            .ToList();

            // Get all booked slots of the sub courts above on Play Date
            var bookedSlotsOnPlayDate = _context.Bookings.Include(b => b.RefOrderNavigation)
                                                         .Include(b => b.RefSlotNavigation)
                                                         .Where(b => b.RefOrderNavigation.IsPaid
                                                                 && !b.RefOrderNavigation.IsRefunded
                                                                 && !b.RefOrderNavigation.IsCanceled
                                                                 &&  b.PlayDate.CompareTo(dto.PlayDate.ToDateTime(TimeOnly.MinValue)) == 0
                                                                 &&  atvSubCourts.Select(sc => sc.Id).Contains(b.RefSubCourt)
                                                                 &&  b.RefSlotNavigation.StartTime.CompareTo(startTime) >= 0
                                                                 &&  b.RefSlotNavigation.EndTime.CompareTo(endTime) <= 0)
                                                         .Select(b => new
                                                         {
                                                             RefSlot = b.RefSlot,
                                                             RefSubCourt = b.RefSubCourt
                                                         }).ToList();

            // Set IsActive status to map with its available status            
            foreach (var sc in atvSubCourts)
            {
                var atvSCSlotsOfCurrSubCourt = atvSlotsOfSubCourts.Where(sl => sl.RefSubCourt == sc.Id);
                var bookedSlotsOfCurrSubCourt = bookedSlotsOnPlayDate.Where(bs => bs.RefSubCourt == sc.Id);

                if (atvSCSlotsOfCurrSubCourt.Count() == 0
                 || atvSCSlotsOfCurrSubCourt.Count() == bookedSlotsOfCurrSubCourt.Count())
                    sc.IsActive = false;
            }

            return Task.FromResult(atvSubCourts.AsEnumerable());            
        }

        public Task<Account> GetCourtOwnerBySubCourtId(string subCourtId)
        {
            var existSubCourt = _context.SubCourts.Find(subCourtId);

            if (existSubCourt == null) return null!;

            var existCourt = _context.Courts.Find(existSubCourt.ParentCourtId);

            if (existCourt == null) return null!;

            var existOwner = _context.Accounts.Find(existCourt.OwnerId);

            if (existOwner == null) return null!;

            return Task.FromResult(existOwner);
        }

        public Task<string> GetCourtNameBySubCourtId(string subCourtId)
        {
            var existSubCourt = _context.SubCourts.Find(subCourtId);

            if (existSubCourt == null) return null!;

            var existCourt = _context.Courts.Find(existSubCourt.ParentCourtId);

            if (existCourt == null) return null!;

            return Task.FromResult(existCourt.Name);
        }

        public Task<IEnumerable<SubCourt>> GetSubCourtsOfOwner(string ownerId)
        {
            var courtsOfOwner = _context.Courts.Where(c => c.OwnerId == ownerId);

            var subCourtsOfOwner = new List<SubCourt>();

            foreach (var court in courtsOfOwner)
            {
                subCourtsOfOwner.AddRange(_context.SubCourts.Where(sc => sc.ParentCourtId == court.Id));
            }

            return Task.FromResult(subCourtsOfOwner.AsEnumerable());
        }
    }
}

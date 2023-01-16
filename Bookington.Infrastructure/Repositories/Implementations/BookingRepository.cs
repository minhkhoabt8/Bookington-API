using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking, BookingtonDbContext>, IBookingRepository
    {
        public BookingRepository(BookingtonDbContext context) : base(context)
        {
        }

        // Not working for some reasons
        /*public async Task<IEnumerable<Booking>> GetBookingsOfSubCourt(string subCourtId, bool trackChanges = false) 
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return await Task.FromResult(dbSet.Include(b => b.RefSlotNavigation)
                .Include(b => b.BookByNavigation)
                .Include(b => b.VoucherCodeNavigation)
                .Where(b => b.RefSlotNavigation.RefSubCourt == subCourtId)
                .AsEnumerable());
        }*/

        public Task<IEnumerable<Booking>> GetBookingsOfSubCourts(List<string> subCourtIds, bool trackChanges = false)
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var result = new List<Booking>();
            foreach (var subCourtId in subCourtIds)
            {
                result.AddRange(dbSet.Include(b => b.RefSlotNavigation)
                .Include(b => b.BookByNavigation)
                .Include(b => b.VoucherCodeNavigation)
                .Where(b => b.RefSlotNavigation.Id == subCourtId)
                .ToList());
            }

            result = result.OrderByDescending(b => b.BookAt).ToList();

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<Booking>> GetBookingsOfUser(string userId, bool trackChanges = false)
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            return Task.FromResult(dbSet.Include(b => b.RefSlotNavigation)
                .Include(b => b.BookByNavigation)
                .Include(b => b.VoucherCodeNavigation)
                .Where(b => b.BookBy == userId)
                .AsEnumerable());
        }

        public async Task<bool> IsCustomerAvailableForCommenting(string userId, bool trackChanges = false)
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var userBookings = await GetBookingsOfUser(userId);

            // Check for bookings customer had paid, not refunded and had passedd the play date
            var playedUserBookings = new List<Booking>();            

            foreach (var b in userBookings)
            {
                if ((b.IsPaid ?? false) && (!b.IsCanceled ?? false) /*&& DateTime.Now.CompareTo(b.IsPaid)*/)
                {
                    playedUserBookings.Add(b);
                }
            }

            var result = false;

            if (playedUserBookings.Count != 0) result = true;

            return result;
        }
    }
}

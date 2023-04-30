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
using Microsoft.AspNetCore.Mvc;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking, BookingtonDbContext>, IBookingRepository
    {
        public BookingRepository(BookingtonDbContext context) : base(context)
        {
        } 

        public Task<IEnumerable<Booking>> GetBookingsOfSubCourts(List<string> subCourtIds, bool trackChanges = false)
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }            

            var result = new List<Booking>();
            foreach(var subCourtId in subCourtIds)
            {
                result.AddRange(dbSet.Include(b => b.RefSlotNavigation)
                                     .Include(b => b.RefSubCourtNavigation)
                                     .Include(b => b.BookByNavigation) 
                                     .Include(b => b.RefOrderNavigation)
                                     .Where(b => !b.RefOrderNavigation.IsCanceled
                                              && !b.RefOrderNavigation.IsRefunded
                                              && b.RefOrderNavigation.IsPaid
                                              && subCourtId == b.RefSubCourt)                                              
                                     .ToList());
            }

            result = result.OrderByDescending(b => b.BookAt).ToList();

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<Booking>> GetBookingsOfOrder(string orderId, bool trackChanges = false)
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var result = new List<Booking>();

            result.AddRange(dbSet.Include(b => b.RefSlotNavigation).Include(b => b.RefSubCourtNavigation)
                .Where(b => b.RefOrder == orderId).ToList());

            result = result.OrderByDescending(b => b.RefSlotNavigation.StartTime).ToList();

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<Booking>> GetIncomingBookingsOfUser(string userId)
        {
            //Get All Incoming Bookings Of A User
            var dbSet = _context.Bookings
                .Include(b => b.RefSlotNavigation)
                .Include(b => b.RefSubCourtNavigation)
                .Include(b => b.RefSubCourtNavigation.ParentCourt)
                .Include(b => b.RefOrderNavigation)
                .ToList()
                .Where(b => b.BookBy == userId
                         && b.RefOrderNavigation.IsPaid
                         && !b.RefOrderNavigation.IsCanceled
                         && !b.RefOrderNavigation.IsRefunded
                         && b.IsCancel == false
                         && b.PlayDate.Add(b.RefSlotNavigation.EndTime).CompareTo(DateTime.Now) > 0)
                .OrderBy(b => b.PlayDate.Date).ThenBy(b => b.RefSlotNavigation.StartTime);

            return Task.FromResult(dbSet.AsEnumerable());            
        }

        public Task<IEnumerable<Booking>> GetFinishedBookingsOfUser(string userId)
        {
            //Get All Finished Bookings Of A User
            var dbSet = _context.Bookings
                .Include(b => b.RefSlotNavigation)
                .Include(b => b.RefSubCourtNavigation)
                .Include(b => b.RefSubCourtNavigation.ParentCourt)
                .Include(b => b.RefOrderNavigation)
                .ToList()
                .Where(b => b.BookBy == userId
                         && b.RefOrderNavigation.IsPaid
                         && !b.RefOrderNavigation.IsCanceled
                         && !b.RefOrderNavigation.IsRefunded
                         && b.IsCancel == true
                         && b.PlayDate.Add(b.RefSlotNavigation.EndTime).CompareTo(DateTime.Now) <= 0)
                .OrderByDescending(b => b.PlayDate.Date).ThenByDescending(b => b.RefSlotNavigation.StartTime);

            return Task.FromResult(dbSet.AsEnumerable());
        }


        public async Task<IEnumerable<Booking>> GetAllBookingOfOrderAsync(string orderId)
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();

            return await Task.FromResult(dbSet.Where(b => b.RefOrder == orderId)
                .AsEnumerable());
        }
    }
}

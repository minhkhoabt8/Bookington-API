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

        public async Task<IEnumerable<Booking>> GetBookingsOfSubCourt(string subCourtId, bool trackChanges = false) 
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
        }

        public Task<IEnumerable<Booking>> GetBookingsOfSubCourts(List<string> subCourtIds, bool trackChanges = false)
        {
            IQueryable<Booking> dbSet = _context.Set<Booking>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var id = subCourtIds[0];

            var result = new List<Booking>();
            foreach(var subCourtId in subCourtIds)
            {
                result.AddRange(dbSet.Include(b => b.RefSlotNavigation)
                .Include(b => b.BookByNavigation)
                .Include(b => b.VoucherCodeNavigation)
                .Where(b => b.RefSlotNavigation.Id == id)
                .ToList());
            }

            result = result.OrderByDescending(b => b.BookAt).ToList();

            return Task.FromResult(result.AsEnumerable());
        }
    }
}

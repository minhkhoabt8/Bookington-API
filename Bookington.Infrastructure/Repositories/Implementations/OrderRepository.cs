using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order, BookingtonDbContext>, IOrderRepository
    {
        public OrderRepository(BookingtonDbContext context) : base(context)
        {
        }

        public bool IsOrderYours(string accountId, string orderId)
        {
            var currOrder = _context.Orders.SingleOrDefault(o => o.Id == orderId);

            if (currOrder == null) return false;

            if (currOrder.CreateBy != accountId) return false;

            return true;
        }

        public bool IsOrderFromYourCourts(string accountId, string orderId)
        {
            var currOrder = _context.Orders.SingleOrDefault(o => o.Id == orderId);

            if (currOrder == null) return false;

            var bookingsFromCurrOrder = _context.Bookings.Where(b => b.RefOrder == orderId)
                                                         .Include(b => b.RefSubCourtNavigation)
                                                         .Include(b => b.RefSubCourtNavigation.ParentCourt).ToList();

            if (bookingsFromCurrOrder.IsNullOrEmpty()) return false;

            var parentCourt = bookingsFromCurrOrder[0].RefSubCourtNavigation.ParentCourt;

            if (parentCourt.OwnerId != accountId) return false;

            return true;
        }

        public Order GetOrderDetailsById(string orderId)
        {
            return _context.Orders.Include(o => o.Transaction)
                                  .Include(o => o.CreateByNavigation)
                                  .Include(o => o.VoucherCodeNavigation)
                                  .Include(o => o.Bookings)
                                  .SingleOrDefault(o => o.Id == orderId)!;
        }


        public async Task<IEnumerable<Order>> GetAllOrderOfUserAsync(string userId)
        {
            return await _context.Orders.Where(o => o.CreateBy == userId)
                                  .Include(o => o.Transaction)
                                  .Include(o => o.CreateByNavigation)
                                  .Include(o => o.VoucherCodeNavigation)
                                  .Include(o => o.Bookings)
                                  .Include(o => o.Bookings).ThenInclude(c => c.RefSlotNavigation)
                                  .Include(o => o.Bookings).ThenInclude(c => c.RefSubCourtNavigation).ThenInclude(s => s.ParentCourt)
                                  .ToListAsync();
        }


        public async Task<IEnumerable<Order>> GetAllOrderForStatistic()
        {
            return await _context.Orders
                                  .Include(o => o.Transaction)
                                  .Include(o => o.CreateByNavigation)
                                  .Include(o => o.Bookings).ThenInclude(c => c.RefSubCourtNavigation).ThenInclude(s => s.ParentCourt)
                                  .ToListAsync();
        }


        public async Task<IEnumerable<Order>> GetAllOrderOfOwnerForStatistic(string ownerId)
        {
           return  await _context.Orders

                                  .Include(o => o.Transaction)
                                  .Include(o => o.CreateByNavigation)
                                  .Include(o => o.Bookings)
                                        .ThenInclude(c => c.RefSubCourtNavigation)
                                            .ThenInclude(s => s.ParentCourt)
                                  .Where(c=>c.Bookings.Any(c=>c.RefSubCourtNavigation.ParentCourt.OwnerId == ownerId))
                                  .ToListAsync();
        }
    }
}

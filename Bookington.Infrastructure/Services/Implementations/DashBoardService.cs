using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.DashBoard;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;

        public DashBoardService(IOrderService orderService, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminDashBoardDTO> GetAdminDashBoard(DashBoardQuery query)
        {

            var orders = await _orderService.GetAllAsync();

            if (query.StartTime != null && query.EndTime != null)
            {
                DateTime startTime = DateTime.Parse(query.StartTime);
                DateTime endTime = DateTime.Parse(query.EndTime);
                orders = orders.Where(o => o.OrderAt >= startTime && o.OrderAt <= endTime);
            }


            return new AdminDashBoardDTO
            {
                TotalOrders = orders.Count(),
                PaidOrders = orders.Count(o => o.IsPaid),
                CanceledOrders = orders.Count(o => o.IsCanceled),
                RefundedOrders = orders.Count(o => o.IsRefunded),
                TotalSales = orders.Where(c => c.IsPaid).Sum(o => o.TotalPrice),
                AverageSale = orders.Where(c => c.IsPaid).Average(o => o.TotalPrice),
                CommissionEarned = orders.Where(o=>o.IsPaid).Sum(o=>o.TotalPrice * 0.05)
            };
        }

        public async Task<OwnerDashBoardDTO> GetOwnerDashBoard(string ownerId, DashBoardQuery query)
        {
            //// Get all orders of the owner
            //var orders = await _context.Orders
            //    .Where(o => o.CreateBy == ownerId)
            //    .Include(o => o.Bookings)
            //        .ThenInclude(b => b.RefSubCourtNavigation)
            //            .ThenInclude(sc => sc.ParentCourt)
            //    .ToListAsync();


            //if (query.StartTime != null && query.EndTime != null)
            //{
            //    DateTime startTime = DateTime.Parse(query.StartTime);
            //    DateTime endTime = DateTime.Parse(query.EndTime);
            //    orders = orders.Where(o => o.OrderAt >= startTime && o.OrderAt <= endTime);
            //}
            throw new NotImplementedException();
        }
    }
}

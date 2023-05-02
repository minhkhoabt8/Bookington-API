using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.DashBoard
{
    public class AdminDashBoardDTO
    {
        public int TotalOrders { get; set; }//an integer value that represents the total number of orders.

        public int PaidOrders { get; set; }//an integer value that represents the number of orders that have been paid.

        public int CanceledOrders { get; set; }// can integer value that represents the number of orders that have been canceled.

        public int RefundedOrders { get; set; }// an integer value that represents the number of orders that have been refunded.

        public double TotalSales { get; set; }//a decimal value that represents the total sales generated from all the orders.

        public double AverageSale { get; set; } // a decimal value that represents the average sales per order. It is calculated by dividing the TotalSales by the TotalOrders.

        public double CommissionEarned { get; set; }  // new field for commission earned by admin for get money from Total Price Of Order

    }
}

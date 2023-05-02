using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.DashBoard
{
    public class OwnerDashBoardDTO
    {
        public int TotalOrders { get; set; }
        public int RefundedOrders { get; set; }
        public int ApprovedOrders { get; set; }
        public int RejectedOrders { get; set; }
        public int TotalBookings { get; set; }
        public double TotalIncomes { get; set; }
        public double CommissionPaid { get; set; }
        public double TotalEarnings { get; set; }
        public double AverageEarnings { get; set; }
        public double TotalRevenue { get; set; }
        public double AverageRevenue { get; set; }
        public double RefundRevenue { get; set; }
    }
}

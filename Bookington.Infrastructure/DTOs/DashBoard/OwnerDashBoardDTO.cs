using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.DashBoard
{
    public class OwnerDashBoardDTO
    {
        public float CourtIncome { get; set; }

        public float DailyInCome { get; set; }

        public float TotalBookingOfCourt { get; set; }

        public string PopularBookingTimeSlotsOfCourt { get; set; }

        public float TotalBooking { get; set; }

        public float TotalSuccessfulBooking { get; set; }

        public float TotalCancelBooking { get; set; }
    }
}

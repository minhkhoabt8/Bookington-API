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
        public float TotalIncomeInSystem { get; set; }

        public float TotalBookinInSystemg { get; set; }
        
        public float TotalSuccessfulBookingInSystem { get; set; }

        public float TotalCancelBookingInSystem { get; set; }

        public float NumberOfReport { get; set; }

        public float NumberOfUnhandleReport { get; set; }   

        public float NumberOfHandleReport { get; set; }

        public string PopularBookingTimeSlotsInSystem { get; set; } 

        public string PopularBookingCourtInSystem { get; set; }
    }
}

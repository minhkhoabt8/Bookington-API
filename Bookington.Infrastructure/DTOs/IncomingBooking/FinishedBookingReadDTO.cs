using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.IncomingBooking
{
    public class FinishedBookingReadDTO
    {
        // ten san
        // ten sub court
        // slot
        // thoi gian dien ra
        // status : incoming, started, finished        

        public string CourtName { get; set; } = null!;

        public string SubCourtName { get; set; } = null!;

        public DateTime PlayDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Status { get; set; } = "finished";
    }
}

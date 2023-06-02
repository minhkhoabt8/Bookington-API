using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Booking
{
    public class BookingReadDTO
    {
        public string? Id { get; set; }

        public string RefSlot { get; set; } = null!;

        public string RefOrder { get; set; } = null!;

        public string BookBy { get; set; } = null!;

        public DateTime BookAt { get; set; }

        public DateTime PlayDate { get; set; }

        public double Price { get; set; }

        public bool IsCancel { get; set; }
    }

    public class CourtBookingHistoryReadDTO
    {
        public string? Id { get; set; }

        public string? SubCourtName { get; set; }

        public string? TimeSlot { get; set; }

        public string? Customer { get; set; }

        public string? Phone { get; set; }

        public DateTime? BookAt { get; set; }

        public double? Price { get; set; }
    }    

    public class BookingForOrderReadDTO
    {
        public string? Id { get; set; }

        public string? SubCourtName { get; set; }

        public string? SubCourtId { get; set; }

        public DateTime PlayDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public double Price { get; set; }

        public bool IsCancel { get; set; }  
    }

}

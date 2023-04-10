using Bookington.Infrastructure.DTOs.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Booking
{
    public class BookingHistoryQuery : PaginatedQuery
    {
        public string CourtId { get; set; } = null!;
    }

    public class IncomingBookingQuery : PaginatedQuery
    {
    }

    public class FinishedBookingQuery : PaginatedQuery
    {
    }
}

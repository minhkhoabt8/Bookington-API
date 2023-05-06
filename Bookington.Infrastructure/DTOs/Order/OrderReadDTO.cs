using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Order
{
    public class OrderReadDTO
    {
        public string Id { get; set; }

        public string CreateBy { get; set; } 

        public string TransactionId { get; set; }

        public string? CourtName { get; set; } 

        public string VoucherCode { get; set; } 

        public DateTime OrderAt { get; set; }

        public double OriginalPrice { get; set; }

        public double TotalPrice { get; set; }

        public bool IsPaid { get; set; } 

        public bool IsCanceled { get; set; } 

        public bool IsRefunded { get; set; } 

        public bool CanBeCanceled { get; set; } = true;

        public ICollection<BookingForOrderReadDTO> Bookings { get; set; } = null!;
        
    }
    public class OrderQuery : PaginatedQuery
    {
        public string UserId { get; set; }
    }
}

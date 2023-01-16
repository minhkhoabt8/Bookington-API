using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Booking
{
    public class BookingWriteDTO
    {        
        public string RefSlot { get; set; } = null!;

        // will be disposed later since authorization is not implemented yet
        public string BookBy { get; set; } = null!;

        public string? VoucherCode { get; set; }               
        
        public DateTime PlayDate { get; set; }
    }

    public class DebugBookingWriteDTO 
    {
        public string? Id { get; set; }

        public string? RefSlot { get; set; }

        public string? BookBy { get; set; }

        public string? VoucherCode { get; set; }        

        public double? Price { get; set; }

        public double? OriginalPrice { get; set; }

        public bool? IsPaid { get; set; } 

        public bool? IsCanceled { get; set; } 

        public bool? IsRefunded { get; set; } 
    }
}

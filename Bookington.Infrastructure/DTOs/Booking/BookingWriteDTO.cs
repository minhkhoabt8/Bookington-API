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
    }
}

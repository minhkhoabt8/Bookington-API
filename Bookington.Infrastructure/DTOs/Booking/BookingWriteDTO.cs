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

        public DateTime PlayDate { get; set; }
    }
}

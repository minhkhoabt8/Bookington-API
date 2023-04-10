using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Booking
{
    public class BookingWriteDTO
    {
        [Required]
        public string RefSubCourt { get; set; } = null!;

        [Required]
        public string RefSlot { get; set; } = null!;

        [Required]
        public DateTime PlayDate { get; set; }        
    }    
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Slot
{
    public class SlotQueryForBooking
    {
        [Required]
        public string SubCourtId { get; set; } = null!;

        [Required]
        public DateOnly PlayDate { get; set; }
    }
}

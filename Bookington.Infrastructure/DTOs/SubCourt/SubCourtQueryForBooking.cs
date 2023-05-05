using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.SubCourt
{
    public class SubCourtQueryForBooking
    {
        [Required]
        public string CourtId { get; set; } = null!;

        [Required]        
        public DateOnly PlayDate { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; } = TimeOnly.MaxValue;
    }
}

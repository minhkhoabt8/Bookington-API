using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Microsoft.Identity.Client;

namespace Bookington.Infrastructure.DTOs.Slot
{
    public class SlotReadDTO
    {
        public string Id { get; set; } = null!;

        public string? RefSubCourt { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool? IsActive { get; set; }
    }

    public class SlotForBookingReadDTO
    {
        public string Id { get; set; } = null!;

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public double Price { get; set; }

        public bool? IsAvailable { get; set; }
    }

    public class SlotsForBookingReadDTO
    {
        public DateOnly PlayDate { get; set; }

        public virtual ICollection<SlotForBookingReadDTO> Slots { get; set; } = null!;
    }
}

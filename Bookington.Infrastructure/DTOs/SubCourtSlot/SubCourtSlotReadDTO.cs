using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.SubCourtSlot
{
    public class SubCourtSlotScheduleReadDTO
    {
        public string Id { get; set; } = null!;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string DaysInSchedule { get; set; } = null!;

        public double Price { get; set; }

        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;

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
}

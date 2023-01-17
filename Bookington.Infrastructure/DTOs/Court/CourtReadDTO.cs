using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Court
{
    public class CourtReadDTO
    {
        public string? OwnerId { get; set; }

        public int? DistrictId { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public TimeSpan? OpenAt { get; set; }

        public TimeSpan? CloseAt { get; set; }
    }
}

using Bookington.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bookington.Infrastructure.Constants.TimeMapping;

namespace Bookington.Infrastructure.DTOs.Court
{
    
    public class CourtWriteDTO
    {
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string DistrictId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string OpenAt { get; set; }
        [Required]
        public string CloseAt { get; set; }
        
    }
}

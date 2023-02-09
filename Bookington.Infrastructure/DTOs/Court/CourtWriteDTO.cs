using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Court
{
    
    public class CourtWriteDTO
    {
        public string OwnerId { get; set; }

        public string DistrictId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string OpenAt { get; set; }
        
        public string CloseAt { get; set; } 
    }
}

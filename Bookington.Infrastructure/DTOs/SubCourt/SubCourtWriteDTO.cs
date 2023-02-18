using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.SubCourt
{
    public class SubCourtWriteDTO
    {
        public string ParentCourtId { get; set; }
        public string Name { get; set; }
        public int CourtTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

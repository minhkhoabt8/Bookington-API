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

        public int DistrictId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "{hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan OpenAt { get; set; }
        [DisplayFormat(DataFormatString = "{hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan CloseAt { get; set; }
    }
}

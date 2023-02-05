using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.District
{
    public class DistrictReadDTO
    {
        public string Id { get; set; } = null!;

        public string ProvinceId { get; set; }

        public string DistrictName { get; set; }
    }
}

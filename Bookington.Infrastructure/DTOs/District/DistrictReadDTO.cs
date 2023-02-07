using Bookington.Infrastructure.DTOs.Province;
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

        public string DistrictName { get; set; }

        public string ProvinceId { get; set; }
    }
}

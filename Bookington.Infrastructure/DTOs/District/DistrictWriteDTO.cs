using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.District
{
    public class DistrictWriteDTO
    {
        public string? Id { get; set; }
        public string? ProvinceId { get; set; }

        public string? DistrictName { get; set; }
    }

    public class DistrictSyncDTO
    {
        public string name { get; set; }
        public int code { get; set; }
        public int province_code { get;set; }
    }
}

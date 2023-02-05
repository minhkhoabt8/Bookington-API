using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Province
{
    public class ProvinceWriteDTO
    {
        public string? Id { get; set; }
        public string? ProvinceName { get; set; }
    }
    public class ProvinceSyncDTO
    {
        public string name { get; set; }
        public int code { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Province
{
    public class ProvinceReadDTO
    {
        public string Id { get; set; } = null!;

        public string? ProvinceName { get; set; }
    }
}

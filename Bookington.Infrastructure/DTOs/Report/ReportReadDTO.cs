using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Report
{
    public class ReportReadDTO
    {
        public string Id { get; set; } = null!;

        public int? TypeId { get; set; }

        public string? ReporterId { get; set; }

        public string? Content { get; set; }
    }
}

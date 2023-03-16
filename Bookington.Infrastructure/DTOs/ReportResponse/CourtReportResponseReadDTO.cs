using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.ReportResponse
{
    public class CourtReportResponseReadDTO
    {
        public string Id { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}

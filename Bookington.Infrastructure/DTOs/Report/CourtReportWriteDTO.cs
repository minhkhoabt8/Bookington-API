using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Report
{
    public class CourtReportWriteDTO
    {        
        public string RefCourt { get; set; } = null!;

        public string ReporterId { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}

using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Report
{
    public class CourtReportReadDTO
    {
        public string Id { get; set; }

        public string RefCourt { get; set; } 

        public string CourtName { get; set; } //

        public string ReporterId { get; set; } 

        public string ReporterPhone { get; set; } // 

        public string ReporterName { get; set; } //

        public string Content { get; set; }

        public bool IsBan { get; set; } = false;
    }
}

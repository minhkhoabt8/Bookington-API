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
        public string Id { get; set; } = null!;

        public string RefCourt { get; set; } = null!;

        public string CourtName { get; set; } = null!;

        public string ReporterId { get; set; } = null!;

        public string ReporterPhone { get; set; } = null!;

        public string ReporterName { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Report
{
    public class UserReportReadDTO
    {
        public string Id { get; set; } = null!;

        public string RefUser { get; set; } = null!;

        public string RefUserName { get; set; } = null!;        

        public string ReporterId { get; set; } = null!;

        public string ReporterCourtName { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}

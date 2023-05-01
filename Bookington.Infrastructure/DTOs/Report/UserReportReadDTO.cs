using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Report
{
    public class UserReportReadDTO
    {
        public string Id { get; set; }

        public string RefUser { get; set; } 

        public string RefUserName { get; set; }    //

        public string ReporterId { get; set; } 

        public string ReporterCourtName { get; set; }

        public string Content { get; set; }
    }
}

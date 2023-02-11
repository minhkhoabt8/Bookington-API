using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Report
{
    public class UserReportCreateDTO
    {
        public string RefUser { get; set; } = null!;        

        public string Content { get; set; } = null!;
    }
}

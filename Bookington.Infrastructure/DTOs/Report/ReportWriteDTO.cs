using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Report
{
    public class ReportWriteDTO
    {        
        public int TypeId { get; set; }

        public string ReporterId { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.ReportResponse
{
    public class CourtReportResponseWriteDTO
    {
        [Required]
        public string CourtReportId { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public bool IsBanned { get; set; } = false;

        public int Duration { get; set; } = 0;
    }
}

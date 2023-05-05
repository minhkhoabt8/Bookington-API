using Bookington.Infrastructure.DTOs.ApiResponse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.SubCourt
{
    public class SubCourtQuery : PaginatedQuery
    {
        [Required]
        public string CourtId { get; set; }

        public string? Search { get; set; }
    }
}

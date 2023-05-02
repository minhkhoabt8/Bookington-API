using Bookington.Infrastructure.DTOs.ApiResponse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Court
{
    public class CourtQueryByDateAndTime : PaginatedQuery
    {
        [Required]
        public string PlayDate { get; set; }
        [Required]
        public string PlayTime { get; set; }
    }
}

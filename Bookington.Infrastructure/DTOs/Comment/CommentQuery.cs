using Bookington.Infrastructure.DTOs.ApiResponse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Comment
{
    public class CommentQuery : PaginatedQuery
    {
        [Required]
        public string CourtId { get; set; }
    }
}

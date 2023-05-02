using Bookington.Infrastructure.DTOs.ApiResponse;
using System.ComponentModel.DataAnnotations;


namespace Bookington.Infrastructure.DTOs.Booking
{
    public class GetBookingsOfSubCourtPaginatedQuery : PaginatedQuery
    {
        [Required]
        public string SubCourtId { get; set; }
    }
}

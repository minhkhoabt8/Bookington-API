
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookington.Infrastructure.DTOs.Court
{
    
    public class CourtWriteDTO
    {
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string DistrictId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string OpenAt { get; set; }
        [Required]
        public string CloseAt { get; set; }

    }
}

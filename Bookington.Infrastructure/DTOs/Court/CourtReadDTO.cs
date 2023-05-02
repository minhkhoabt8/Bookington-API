using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Court
{
    public class CourtReadDTO
    {
        public string? Id { get; set; }

        public string? Name { get; set; }        

        public string? Phone { get; set; }

        public string? DistrictName { get; set; }

        public string? ProvinceName { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }

        public double MoneyPerHour { get; set; }

        public int NumberOfSubCourt { get; set; }        

        public double RatingStar { get; set; }

        public int NumOfReview { get; set; }

        public TimeSpan? OpenAt { get; set; }

        public TimeSpan? CloseAt { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<ImageFile>? CourtPictures { get; set; }
    }
}

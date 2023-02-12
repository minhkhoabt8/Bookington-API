using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Court
{
    public class CourtQueryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double RatingStar { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public double MoneyPerHour { get; set; }
        public int NumberOfSubCourt { get; set; }
        public TimeSpan OpenAt { get; set; }
        public TimeSpan CloseAt { get; set; }

        public List<IFormFile> CourtPicture { get; set; }
    }
}

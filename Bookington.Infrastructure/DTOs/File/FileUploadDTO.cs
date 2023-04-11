using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.File
{
    public class FileUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public bool IsAccount { get; set; }
    }
}

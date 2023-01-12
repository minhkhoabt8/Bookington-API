using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Role
{
    public class RoleWriteDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string RoleName { get; set; }
    }
}

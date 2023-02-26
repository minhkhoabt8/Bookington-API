using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Account
{
    public class AccountWriteDTO
    {
        public string Phone { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }
    }

    public class AccountUpdateDTO
    {
        public string? FullName { get; set; }
    }

    public class ChangePasswordDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }

}

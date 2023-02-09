using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Account
{
    public class AccountReadDTO
    {
        public string Id { get; set; }

        public string RoleId { get; set; }

        public string Phone { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }
    }

    public class AccountLoginInputDTO
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class AccountLoginOutputDTO
    {
        public string UserID { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string SysToken { get; set; }
        public int SysTokenExpires { get; set; }
    }

    

    public class ConfirmUserDTO
    {
        public string Phone { get; set; }
        public string OTP { get; set; }
    }
}

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

        public int? RoleId { get; set; }

        public string? Phone { get; set; }

        public string? FullName { get; set; }


        public bool? IsConfirmed { get; set; }

        public bool? IsActive { get; set; }
    }
}

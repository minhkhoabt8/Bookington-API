using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.UserBalance
{
    public class UserBalanceWriteDTO
    {
        public string RefUser { get; set; } = null!;

        public double Balance { get; set; } = 0;
    }
}

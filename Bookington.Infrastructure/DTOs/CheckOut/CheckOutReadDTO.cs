using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.CheckOut
{
    public class CheckOutResponse
    {
        public string TransactionId { get; set; } = null!;

        public string OrderId { get; set; } = null!;
    }
}

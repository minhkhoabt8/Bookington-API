using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.TransactionHistory
{
    public class MomoTransactionWriteDTO
    {
        public string OrderId { get;set; }

        public string TransactionId { get; set; }

        public double Amount { get; set; }

    }
}

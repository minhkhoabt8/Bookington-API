using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.TransactionHistory
{
    public class TransactionHistoryWriteDTO
    {        
        public string RefFrom { get; set; } = null!;

        public string RefTo { get; set; } = null!;

        public double Amount { get; set; }
    }
}

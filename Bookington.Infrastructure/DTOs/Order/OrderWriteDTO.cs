using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Order
{
    public class OrderWriteDTO
    {
        public string Id { get; set; } = null!;

        public string TransactionId { get; set; } = String.Empty;

        public DateTime OrderAt { get; set; }

        public double TotalPrice { get; set; } = 0;

        public bool IsPaid { get; set; } = false;

        public bool IsCanceled { get; set; } = false;

        public bool IsRefunded { get; set; } = false;
    }
}

using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Order
{
    public class OrderReadDTO
    {
        public string Id { get; set; } = null!;

        public string TransactionId { get; set; } = String.Empty;

        public DateTime OrderAt { get; set; }

        public double TotalPrice { get; set; }

        public bool IsPaid { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsRefunded { get; set; }

        public IEnumerable<Bookington.Core.Entities.Booking> Bookings { get; set; } = null!;
    }
}

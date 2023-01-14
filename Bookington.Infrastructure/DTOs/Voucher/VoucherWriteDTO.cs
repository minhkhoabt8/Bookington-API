using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Voucher
{
    public class VoucherWriteDTO
    {
        public string Id { get; set; } = null!;

        public string? CreateBy { get; set; }

        public string? RefCourt { get; set; }

        public string VoucherCode { get; set; } = null!;

        public string? Title { get; set; }

        public string? Description { get; set; }

        public double? Discount { get; set; }

        public int? Usages { get; set; }

        public int? MaxQuantity { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreateAt { get; set; }

        public bool? IsActive { get; set; }
    }
}

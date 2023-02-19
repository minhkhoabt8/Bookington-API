using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Voucher
{
    public class VoucherWriteDTO
    {
        [Required]
        public string? CreateBy { get; set; }
        [Required]
        public string? RefCourt { get; set; }
        [Required]
        public string VoucherCode { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public int MaxQuantity { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = false;
    }
}

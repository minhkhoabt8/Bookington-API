using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Momo
{
    public class MomoPaymentInfo
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public string OrderInfo { get; set; }
    }
}

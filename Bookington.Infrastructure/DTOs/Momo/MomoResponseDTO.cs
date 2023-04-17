using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Momo
{
    public class MomoResponseDTO
    {
        //https://developers.momo.vn/v3/vi/docs/payment/api/wallet/onetime
        public string partnerCode { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public decimal amount { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; }
        public int resultCode { get; set; }
        public string payUrl { get; set; }
        public string deeplink { get; set; }
        public string deeplinkMiniApp { get; set; }
    }
}

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
        public string PartnerCode { get; set; }
        //public string Accesskey { get; set; }
        //public string RequestId { get; set; }
        //public string Amount { get; set; }
        public string OrderId { get; set; }
        public string RequestId { get; set; }
        public string Amount { get; set; }
        public string ResponseTime { get; set; }
        public string Message { get; set; }
        public string ResultCode { get; set; }
        public string PayUrl { get; set; }
        public string DeepLink { get; set; }
        public string QrCodeUrl { get; set; }
    }
}

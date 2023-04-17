using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Momo
{
    //https://developers.momo.vn/v2/#/docs/aiov2/?id=ki%e1%bb%83m-tra-tr%e1%ba%a1ng-th%c3%a1i-giao-d%e1%bb%8bch
    public class MomoCheckTransactionRequestDTO
    {
        public  string PartnerCode { get; set; }    
        public string RequestId { get; set; }
        public string OrderId { get; set; }
        //HMAC_SHA256(accessKey=$accessKey&orderId=$orderId
        //&partnerCode=$partnerCode&requestId=$requestId,secretKey)
        public string Signature { get; set; }
        public string Lang { get; set; } = "en";
    }
}

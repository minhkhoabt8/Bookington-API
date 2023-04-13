using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Momo
{
    public class MomoRequestDTO
    {
        //PARTNER_CODE lấy từ business account đăng ký với MoMo
        public string PartnerCode { get; set; }
        //ACCESS_KEY lấy từ business account đăng ký với MoMo
        public string AccessKey { get; set; }
        //Định danh mỗi yêu cầu
        public string RequestId { get; set; }
        //Số tiền cần thanh toán
        public int Amount { get; set; }
        //Mã đơn hàng cần thanh toán (cần đảm bảo tính duy nhất)
        public string OrderId { get; set; }
        //Trang web mà MoMo sẽ redirect về sau khi user thực hiện thanh toán xong (ví dụ: https://example.com/orders/1)
        public string ReturnUrl { get; set; }
        //Trang web mà MoMo sẽ gửi data về thông qua IPN sau khi user thực hiện thanh toán xong (ví dụ: https://example.com/orders/1)
        public string NotifyUrl { get; set; }
        //captureMoMoWallet
        public string RequestType { get; set; }
        //Chữ ký điện tử để kiểm tra thông tin
        public string Signature { get; set; }
        //Thông tin bổ sung cho order theo định dạng <key>=<value>;<key>=<value>
        //mặc định là ""
        public object[]? ExtraData { get;set; }
    }
}

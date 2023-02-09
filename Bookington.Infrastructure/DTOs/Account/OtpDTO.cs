using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Account
{
    public class OtpDTO
    {
        public string Id { get; set; }
        public string OtpCode { get; set; }
        public DateTime CreateAt { get;set; } 
        public DateTime ExpireAt { get; set; }
        public bool IsConfirmed { get; set; }

        public static OtpDTO GenerateOTP()
        {
            return new OtpDTO()
            {
                Id = Guid.NewGuid().ToString(),
                OtpCode = new Random().Next(100000, 999999).ToString(),
                CreateAt = DateTime.Now,
                ExpireAt = DateTime.Now.AddDays(30),
                IsConfirmed = false
            };
        }

        public static bool ValidateOTP(string checkOTP, OtpDTO otp)
        {
            if (otp.ExpireAt < DateTime.Now || checkOTP != otp.OtpCode)
                return false;
            return true;
        }

    }

}

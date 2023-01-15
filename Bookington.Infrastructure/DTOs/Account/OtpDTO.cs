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
        public string Otp { get; set; }
        public DateTime CreateAt { get;set; }
        public DateTime ExpiredTime { get; set; }
        public bool IsConfirmed { get; set; }

        public static OtpDTO GenerateOTP()
        {
            return new OtpDTO()
            {
                Id = Guid.NewGuid().ToString(),
                Otp = new Random().Next(100000, 999999).ToString(),
                CreateAt = DateTime.Now,
                ExpiredTime = DateTime.Now.AddDays(30),
                IsConfirmed = false
            };
        }

        public static bool ValidateOTP(string checkOTP, OtpDTO otp)
        {
            if (otp.ExpiredTime < DateTime.Now || checkOTP != otp.Otp)
                return false;
            return true;
        }

    }

}

using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bookington_Api.Controllers
{
    [Route("sms/sent")]
    [ApiController]
    public class SmsSpeedController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendSms([Required]string phone)
        {
            SmsSpeedService smsApi = new SmsSpeedService();
            var otp = OtpDTO.GenerateOTP();
            var response = smsApi.sendSmsAsync(phone, otp.Otp);
            return ResponseFactory.Ok(response);
        }

    }
}

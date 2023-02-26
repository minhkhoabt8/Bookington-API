using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Bookington_Api.Controllers
{
    [Route("sms/sent")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class SmsSpeedController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsSpeedController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost("{phone}")]
        public async Task<IActionResult> SendSms([Required]string phone)
        {
            var otp = OtpDTO.GenerateOTP();
            var response = _smsService.sendSmsAsync(phone, otp.OtpCode);
            return ResponseFactory.Ok(response);
        }

    }
}

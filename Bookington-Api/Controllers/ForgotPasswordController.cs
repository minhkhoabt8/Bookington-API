using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Forgot Password Controller
    /// </summary>
    [Route("forgot-password")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>        
        /// </summary>
        public ForgotPasswordController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Verify Phone Number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpPost("verify-phone")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> VerifyAccount([Required] string phoneNumber)
        {

            var account = await _accountService.VerifyPhoneNumber(phoneNumber);

            return ResponseFactory.Ok(account);
        }


        /// <summary>
        /// Verify Otp
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        [HttpPost("verify-otp")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> VerifyOtpt([Required] string phoneNumber, [Required] string otp)
        {

            await _accountService.VerifyAccount(phoneNumber, otp);

            return ResponseFactory.NoContent();
        }

        /// <summary>
        /// Update New Password
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> NewPassword([Required] string phoneNumber, [Required] string newPassword)
        {
            var account = await _accountService.UpdatePassword(phoneNumber, newPassword);
            
            return ResponseFactory.Ok(account);
        }

    }
}

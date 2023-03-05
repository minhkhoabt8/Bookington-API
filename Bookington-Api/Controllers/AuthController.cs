using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Auth Controller
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;
        /// <summary>        
        /// </summary>
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        ///Login
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<AccountLoginOutputDTO>))]
        public async Task<IActionResult> Login(AccountLoginInputDTO input)
        {
            var result = await _accountService.LoginWithPhoneNumber(input);
            
            SetRefreshTokenCookie(result.RefreshToken);
            
            return ResponseFactory.Ok(result);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<AccountLoginOutputDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> Refresh(string? token)
        {
            token ??= Request.Cookies["RefreshToken"] ?? throw new UnauthorizedException();

            var result = await _accountService.LoginWithRefreshTokenAsync(token);

            SetRefreshTokenCookie(result.RefreshToken);

            return ResponseFactory.Ok(result);
        }

        /// <summary>
        /// Resend Otp Code
        /// </summary>
        /// <returns></returns>
        [HttpGet("resend-otp")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        public async Task<IActionResult> ReSendVerifyOtp(string phone)
        {
            await _accountService.ReSendVerifyOtp(phone);
            return ResponseFactory.NoContent();
        }


        private void SetRefreshTokenCookie(string token)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(1),
                HttpOnly = true,
                Secure = true
            };

            Response.Cookies.Append("RefreshToken", token, option);
        }

    }
}

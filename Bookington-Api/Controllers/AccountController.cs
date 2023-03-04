using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    /// 
    [Route("accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>        
        /// </summary>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        /// <summary>
        /// Get All Account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize(AccountRole.admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            var accounts = await _accountService.GetAllAsync();
            return ResponseFactory.Ok(accounts);
        }


        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("signUp")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<AccountReadDTO>))]
        public async Task<IActionResult> CreateAsync(AccountWriteDTO dto)
        {
            var created = await _accountService.CreateAsync(dto);
            return ResponseFactory.Created(created);
        }


        /// <summary>
        /// Verify Account
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        [HttpPut("verify")]
        public async Task<IActionResult> VerifyAccount([Required] string phoneNumber, [Required] string otp)
        {

            await _accountService.VerifyAccount(phoneNumber, otp);

            return NoContent();
        }

        /// <summary>
        /// Update account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="writeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<AccountReadDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateAccount(string id, AccountUpdateDTO writeDTO)
        {

            var account = await _accountService.UpdateAsync(id, writeDTO);

            return ResponseFactory.Ok(account);
        }

        /// <summary>
        /// Delete account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [RoleAuthorize(AccountRole.admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            await _accountService.DeleteAsync(id);

            return ResponseFactory.NoContent();
        }


        /// <summary>
        /// Query accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet("query")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<AccountReadDTO>))]
        public async Task<IActionResult> QueryAccounts([FromQuery] AccountQuery query)
        {
            var accounts = await _accountService.QueryAccountsAsync(query);

            return ResponseFactory.PaginatedOk(accounts);
        }

        /// <summary>
        /// Get Profile From Context (Must Login)
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        [RoleAuthorize(AccountRole.admin, AccountRole.owner, AccountRole.user)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiBadRequestResponse))]
        public async Task<IActionResult> GetProfileAsync()
        {
            var profile = await _accountService.GetProfileAsync();
            return ResponseFactory.Ok(profile);
        }

        /// <summary>
        /// Get Profile By Id For Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile/{accountId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetProfileAsync(string accountId)
        {
            var profile = await _accountService.GetProfileByIdAsync(accountId);
            return ResponseFactory.Ok(profile);
        }

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("change-password")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<ChangePasswordDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdatePassword(ChangePasswordDTO dto)
        {
            await _accountService.ChangePasswordAsync(dto);
            return ResponseFactory.NoContent();
        }

    }
}

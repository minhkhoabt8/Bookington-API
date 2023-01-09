using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLib.ResponseWrapper;

namespace Bokkington_Api.Controllers
{
    [Route("bookington/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Get All Account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccountReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var accounts = await _accountService.GetAllAsync();
            return ResponseFactory.Ok(accounts);
        }
    }
}

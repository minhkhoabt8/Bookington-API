using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
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


        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<AccountReadDTO>))]
        public async Task<IActionResult> CreateAsync(AccountWriteDTO dto)
        {
            var createdTag = await _accountService.CreateAsync(dto);
            return ResponseFactory.Created(createdTag);
        }
    }
}

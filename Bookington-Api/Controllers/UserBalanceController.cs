using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.DTOs.UserBalance;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// User Balance Controller
    /// </summary>    
    [Route("userbalances")]
    [ApiController]
    public class UserBalanceController : ControllerBase
    {
        private IUserBalanceService _userBalanceService;

        /// <summary>        
        /// </summary>
        public UserBalanceController(IUserBalanceService userBalanceService)
        {
            _userBalanceService = userBalanceService;
        }

        /// <summary>
        /// Get Customer's Own Balance
        /// </summary>
        /// <returns></returns>        
        [HttpGet("self")]        
        [RoleAuthorize(AccountRole.admin, AccountRole.owner, AccountRole.user)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetSelfBalance()
        {
            var balance = await _userBalanceService.GetSelfBalance();
            return ResponseFactory.Ok(balance);
        }

        /// <summary>
        /// Get All User Balances
        /// </summary>
        /// <returns></returns>        
        [HttpGet("")]
        [RoleAuthorize(AccountRole.admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            var balances = await _userBalanceService.GetAllAsync();
            return ResponseFactory.Ok(balances);
        }

        /// <summary>
        /// Update User Balance
        /// </summary>
        /// <returns></returns>        
        [HttpPost()]
        [RoleAuthorize(AccountRole.admin, AccountRole.owner, AccountRole.user)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> AddBalance(UserBalanceWriteDTO dto)
        {
            var balance = await _userBalanceService.AddBalanceAsync(dto);
            return ResponseFactory.Ok(balance);
        }
    }
}

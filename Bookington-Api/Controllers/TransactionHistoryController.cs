using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.TransactionHistory;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Transaction History Controller
    /// </summary>
    [Route("transactionhistory")]    
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private ITransactionService _transactionService;

        /// <summary>        
        /// </summary>
        public TransactionHistoryController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Get Customer's Own Transaction History
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>        
        [HttpGet("self")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<IEnumerable<TransactionHistoryReadDTO>>))]
        public async Task<IActionResult> GetSelfTransactionHistory(int? page)
        {           
            var trans = await _transactionService.GetSelfTransactionHistory(page ?? 1);
            return ResponseFactory.Ok(trans);
        }
    }
}

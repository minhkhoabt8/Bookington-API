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
    [Route("transaction-history")]    
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
        /// <param name="query"></param>
        /// <returns></returns>        
        [HttpGet("self")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiPaginatedOkResponse<IEnumerable<TransactionHistoryReadDTO>>))]
        public async Task<IActionResult> GetSelfTransactionHistory([FromQuery] TransactionHistoryQuery query)
        {           
            var trans = await _transactionService.GetSelfTransactionHistory(query);
            return ResponseFactory.PaginatedOk(trans);
        }

        /// <summary>
        /// Get Court Owner's Transaction History
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>        
        [HttpGet("owner")]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiPaginatedOkResponse<IEnumerable<TransactionHistoryReadDTO>>))]
        public async Task<IActionResult> GetOwnerTransactionHistory([FromQuery] TransactionHistoryQuery query)
        {
            var trans = await _transactionService.GetOwnerTransactionHistory(query);
            return ResponseFactory.PaginatedOk(trans);
        }
    }
}

using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.Momo;
using Bookington.Infrastructure.DTOs.TransactionHistory;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Top up Balance
    /// </summary>
    [Route("top-up")]
    [ApiController]
    public class TopUpController : ControllerBase
    {
        private readonly IMomoPaymentService _paymentService;
        private readonly ITransactionService _transactionService;

        public TopUpController(IMomoPaymentService paymentService, ITransactionService transactionService)
        {
            _paymentService = paymentService;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Top-Up Account Wallet
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MomoResponseDTO))]
        public async Task<IActionResult> TopUp(MomoPaymentInfo info)
        {
            var data = await _paymentService.CreatePaymentRequestToMomo(info);

            return ResponseFactory.Ok(data);
        }

        /// <summary>
        /// Handles the Momo checkout response to confirm a top-up.
        /// </summary>
        /// <returns></returns>
        [HttpPost("/confirmTopUpWithMomo")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MomoTransactionReadDTO))]
        public async Task<IActionResult> ConfirmTopUp([FromBody] MomoCheckoutResponseDTO dto)
        {
            var result = await _transactionService.ConfirmTopUp(dto);

            return ResponseFactory.Ok(result);
        }

    }
}

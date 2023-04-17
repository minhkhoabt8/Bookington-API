using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.Momo;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
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

        public TopUpController(IMomoPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Top-Up Account Wallet
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MomoResponseDTO))]
        public async Task<IActionResult> ConfirmTopUp([FromBody] MomoCheckoutResponseDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}

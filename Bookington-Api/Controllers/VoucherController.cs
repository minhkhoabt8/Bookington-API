using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Voucher;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Voucher Controller
    /// </summary>
    [ApiController]
    [Route("vouchers")]
    [RoleAuthorize(AccountRole.owner)]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;
        /// <summary>     
        /// Constructor
        /// </summary>
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        /// <summary>
        /// Create voucher
        /// </summary>
        /// <param name="writeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<VoucherReadDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateVoucher(VoucherWriteDTO writeDTO)
        {
            var voucher = await _voucherService.CreateAsync(writeDTO);

            return ResponseFactory.Created(voucher);
        }
        /// <summary>
        /// Update voucher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="writeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<VoucherReadDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateVoucher(string id, VoucherWriteDTO writeDTO)
        {

            var voucher = await _voucherService.UpdateAsync(id, writeDTO);

            return ResponseFactory.Ok(voucher);
        }

        /// <summary>
        /// Delete voucher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteVoucher(string id)
        {
            await _voucherService.DeleteAsync(id);

            return ResponseFactory.NoContent();
        }


        /// <summary>
        /// Get All Voucher Of a Court
        /// </summary>
        /// <param name="courtId"></param>
        /// <returns></returns>
        [HttpGet("court")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetAllVoucherOfACourtAsync([FromQuery]string courtId)
        {
            var vouchers = await _voucherService.GetAllVoucherOfACourtAsync(courtId);
            return ResponseFactory.Ok(vouchers);
        }
    }
}

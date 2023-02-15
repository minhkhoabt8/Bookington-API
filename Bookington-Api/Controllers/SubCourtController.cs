using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    /// 
    [Route("bookington/subcourts")]
    [ApiController]
    public class SubCourtController : Controller
    {
        private ISubCourtService _subCourtService;

        public SubCourtController(ISubCourtService subCourtService)
        {
            _subCourtService = subCourtService;
        }

        /// <summary>
        /// Update sub court
        /// </summary>
        /// <param name="id"></param>
        /// <param name="writeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<SubCourtReadDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateSubCourt(string id, SubCourtWriteDTO writeDTO)
        {

            var subCourt = await _subCourtService.UpdateAsync(id, writeDTO);

            return ResponseFactory.Ok(subCourt);
        }


        /// <summary>
        /// Create a new sub courts
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<List<SubCourtWriteDTO>>))]
        public async Task<IActionResult> CreateAsync(List<SubCourtWriteDTO> dto)
        {
            var subCourts = await _subCourtService.CreateSubCourtFromListAsync(dto);

            return ResponseFactory.Created(subCourts);
        }

        /// <summary>
        /// Soft delete sub court
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteSubCourt(string id)
        {
            await _subCourtService.DeleteAsync(id);

            return ResponseFactory.NoContent();
        }

        /// <summary>
        /// Get Sub Courts By Court Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{courtId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetSubCourtsOfACourt(string courtId)
        {
            var profile = await _subCourtService.GetSubCourtsOfACourt(courtId);
            return ResponseFactory.Ok(profile);
        }
    }
}

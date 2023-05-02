using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Court Controller
    /// </summary>
    [Route("courts")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtService _courtService;
        private readonly ISubCourtService _subCourtService;
        private readonly ISlotService _slotService;

        /// <summary>        
        /// </summary>
        public CourtController(ICourtService courtService, ISubCourtService subCourtService, ISlotService slotService)
        {
            _courtService = courtService;
            _subCourtService = subCourtService;
            _slotService = slotService;
        }

        /// <summary>
        /// Get All Courts Of Owner
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<CourtReadDTO>))]
        public async Task<IActionResult> GetAllCourtsByOwnerIdAsync([FromQuery] CourtOfOwnerQuery query)
        {
            var courts = await _courtService.GetAllCourtsByOwnerIdAsync(query);
            return ResponseFactory.PaginatedOk(courts);
        }

        /// <summary>
        /// Get a Court details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [RoleAuthorize(AccountRole.owner, AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourtReadDTO))]
        public async Task<IActionResult> GetDetailsAsync(string id)
        {
            var tag = await _courtService.GetByIdAsync(id);
            return ResponseFactory.Ok(tag);
        }

        /// <summary>
        /// Create a new court
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateAsync([FromForm] CourtWriteDTO dto, [Required] IEnumerable<IFormFile> courtImages)
        {
            var createdCourt = await _courtService.CreateAsync(dto, courtImages);
            return ResponseFactory.Created(createdCourt);
        }


        /// <summary>
        /// Update a court
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="courtImages"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [RoleAuthorize(AccountRole.owner)]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateAsync(string id, [FromForm] CourtWriteDTO dto, IEnumerable<IFormFile> courtImages)
        {
            var updatedTag = await _courtService.UpdateAsync(id, dto, courtImages);
            return ResponseFactory.Ok(updatedTag);
        }

        /// <summary>
        /// Delete a court   
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [RoleAuthorize( AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _courtService.DeleteAsync(id);
            return ResponseFactory.NoContent();
        }


        /// <summary>
        /// Query courts
        /// </summary>
        /// <returns></returns>
        [HttpGet("query")]
        [RoleAuthorize(AccountRole.owner, AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<CourtQueryResponse>))]
        public async Task<IActionResult> QueryCourts([FromQuery] CourtItemQuery query)
        {
            var courts = await _courtService.QueryCourtsAsync(query);

            return ResponseFactory.PaginatedOk(courts);
        }

        ///// <summary>
        ///// Get Courts With Available Slots
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("available-slot")]
        //[RoleAuthorize(AccountRole.owner, AccountRole.customer)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<CourtQueryResponse>))]
        //public async Task<IActionResult> GetCourtsWithAvailableSlots([FromQuery] CourtQueryByDateAndTime query)
        //{
        //    var courts = await _courtService.GetCourtsWithAvailableSlots(query);

        //    return ResponseFactory.PaginatedOk(courts);
        //}
    }
}

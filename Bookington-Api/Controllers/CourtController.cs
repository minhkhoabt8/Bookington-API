using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.DTOs.Slot;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// <returns></returns>
        [HttpGet]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourtReadDTO>))]
        public async Task<IActionResult> GetAllCourtByOwnerIdAsync()
        {
            var courts = await _courtService.GetAllCourtByOwnerIdAsync();
            return ResponseFactory.Ok(courts);
        }

        /// <summary>
        /// Get a Court details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [RoleAuthorize(AccountRole.owner, AccountRole.user)]
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
        public async Task<IActionResult> CreateAsync(CourtWriteDTO dto)
        {
            var createdCourt = await _courtService.CreateAsync(dto);
            return ResponseFactory.Created(createdCourt);
        }


        /// <summary>
        /// Update a court
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [RoleAuthorize(AccountRole.owner)]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateAsync(string id, CourtWriteDTO dto)
        {
            var updatedTag = await _courtService.UpdateAsync(id, dto);
            return ResponseFactory.Ok(updatedTag);
        }

        /// <summary>
        /// Delete a court
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [RoleAuthorize( AccountRole.owner)]
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
        [RoleAuthorize(AccountRole.owner, AccountRole.user)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<CourtQueryResponse>))]
        public async Task<IActionResult> QueryCourts([FromQuery] CourtItemQuery query)
        {
            var courts = await _courtService.QueryCourtsAsync(query);

            return ResponseFactory.PaginatedOk(courts);
        }        
    }
}

using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Court Controller
    /// </summary>
    [Route("bookington/courts")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtService _courtService;

        /// <summary>        
        /// </summary>
        public CourtController(ICourtService courtService)
        {
            _courtService = courtService;
        }

        /// <summary>
        /// Get All Court
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourtReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var tags = await _courtService.GetAllAsync();
            return ResponseFactory.Ok(tags);
        }

        /// <summary>
        /// Get a Court details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id:int}")]
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
        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateAsync(int id, CourtWriteDTO dto)
        {
            var updatedTag = await _courtService.UpdateAsync(id, dto);
            return ResponseFactory.Ok(updatedTag);
        }

        /// <summary>
        /// Delete a court
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _courtService.DeleteAsync(id);
            return ResponseFactory.NoContent();
        }


        /// <summary>
        /// Query courts
        /// </summary>
        /// <returns></returns>
        [HttpGet("query")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<CourtReadDTO>))]
        public async Task<IActionResult> QueryCourts([FromQuery] CourtItemQuery query)
        {
            var courts = await _courtService.QueryCourtsAsync(query);

            return ResponseFactory.PaginatedOk(courts);
        }

    }
}

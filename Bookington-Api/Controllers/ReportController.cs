using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Report Controller
    /// </summary>
    [Route("bookington/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        /// <summary>        
        /// </summary>
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // COURT REPORTS RELATED API CALLS

        /// <summary>
        /// Get All Court Reports
        /// </summary>
        /// <returns></returns>
        [HttpGet("courtreports")]               
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourtReportReadDTO>))]
        public async Task<IActionResult> GetAllCourtReportsAsync()
        {
            var courtReports = await _reportService.GetAllCourtReportsAsync();
            return ResponseFactory.Ok(courtReports);
        }

        /// <summary>
        /// Get A Court Report Details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("courtreports/{id}")]                
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourtReportReadDTO))]
        public async Task<IActionResult> GetCourtReportDetailsAsync(string id)
        {
            var report = await _reportService.GetCourtReportByIdAsync(id);
            return ResponseFactory.Ok(report);
        }

        /// <summary>
        /// Create A New Court Report
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("courtreports")]                
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateCourtReportAsync(CourtReportWriteDTO dto)
        {
            var newReport = await _reportService.CreateCourtReportAsync(dto);
            return ResponseFactory.Created(newReport);
        }

        /// <summary>
        /// Update A Court Report
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("courtreports/{id}")]                
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateCourtReportAsync(string id, CourtReportWriteDTO dto)
        {
            var updatedReport = await _reportService.UpdateCourtReportAsync(id, dto);
            return ResponseFactory.Ok(updatedReport);
        }

        /// <summary>
        /// Delete A Court Report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("courtreports/{id}")]                
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteCourtReportAsync(string id)
        {
            await _reportService.DeleteCourtReportAsync(id);
            return ResponseFactory.NoContent();
        }

        //-------------------------------------------------------------------------------------------------

        // USER REPORTS RELATED API CALLS

        /// <summary>
        /// Get All User Reports
        /// </summary>
        /// <returns></returns>
        [HttpGet("userreports")]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserReportReadDTO>))]
        public async Task<IActionResult> GetAllUserReportsAsync()
        {
            var userReports = await _reportService.GetAllUserReportsAsync();
            return ResponseFactory.Ok(userReports);
        }

        /// <summary>
        /// Get A User Report Details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("userreports/{id}")]                
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReportReadDTO))]
        public async Task<IActionResult> GetUserReportDetailsAsync(string id)
        {
            var report = await _reportService.GetUserReportByIdAsync(id);
            return ResponseFactory.Ok(report);
        }

        /// <summary>
        /// Create A New User Report
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("userreports")]             
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateCourtReportAsync(UserReportCreateDTO dto)
        {
            var newReport = await _reportService.CreateUserReportAsync(dto);
            return ResponseFactory.Created(newReport);
        }

        /// <summary>
        /// Update A User Report
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("userreports/{id}")]                
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateUserReportAsync(string id, UserReportUpdateDTO dto)
        {
            var updatedReport = await _reportService.UpdateUserReportAsync(id, dto);
            return ResponseFactory.Ok(updatedReport);
        }

        /// <summary>
        /// Delete A User Report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("userreports/{id}")]               
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteUserReportAsync(string id)
        {
            await _reportService.DeleteUserReportAsync(id);
            return ResponseFactory.NoContent();
        }
    }
}

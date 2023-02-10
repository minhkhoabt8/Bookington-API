using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
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

        /// <summary>
        /// Get All Court Reports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourtReportReadDTO>))]
        public async Task<IActionResult> GetAllCourtReportsAsync()
        {
            var courtReports = await _reportService.GetAllCourtReportsAsync();
            return ResponseFactory.Ok(courtReports);
        }

        /// <summary>
        /// Get A Report Details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
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
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateAsync(CourtReportWriteDTO dto)
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
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteCourtReportAsync(string id)
        {
            await _reportService.DeleteCourtReportAsync(id);
            return ResponseFactory.NoContent();
        }        
    }
}

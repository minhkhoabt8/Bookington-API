using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.DTOs.ReportType;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>  
    /// Report Controller
    /// </summary>
    [Route("api/report")]
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

        //-------------------------------------------------
        //                     Report
        //-------------------------------------------------

        /// <summary>
        /// Get All Reports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReportReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var tags = await _reportService.GetAllAsync();
            return ResponseFactory.Ok(tags);
        }

        /// <summary>
        /// Get A Report Details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportReadDTO))]
        public async Task<IActionResult> GetDetailsAsync(string id)
        {
            var report = await _reportService.GetByIdAsync(id);
            return ResponseFactory.Ok(report);
        }

        /// <summary>
        /// Create A New Report
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> CreateAsync(ReportWriteDTO dto)
        {
            var newReport = await _reportService.CreateAsync(dto);
            return ResponseFactory.Created(newReport);
        }


        /// <summary>
        /// Update A Report
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> UpdateAsync(string id, ReportWriteDTO dto)
        {
            var updatedReport = await _reportService.UpdateAsync(id, dto);
            return ResponseFactory.Ok(updatedReport);
        }

        /// <summary>
        /// Delete A Report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _reportService.DeleteAsync(id);
            return ResponseFactory.NoContent();
        }

        //-------------------------------------------------
        //                   Report Type
        //-------------------------------------------------

        /// <summary>
        /// Get All Report Types
        /// </summary>
        /// <returns></returns>
        [HttpGet("type")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReportTypeReadDTO>))]
        public async Task<IActionResult> GetAllTypesAsync()
        {
            var reportTypes = await _reportService.GetAllTypesAsync();
            return ResponseFactory.Ok(reportTypes);
        }

        /// <summary>
        /// Get A Report Type Details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("type/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportTypeReadDTO))]
        public async Task<IActionResult> GetTypeDetailsAsync(int id)
        {
            var report = await _reportService.GetTypeByIdAsync(id);
            return ResponseFactory.Ok(report);
        }

        /// <summary>
        /// Create A New Report
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("type")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> CreateTypeAsync(ReportTypeWriteDTO dto)
        {
            var newReportType = await _reportService.CreateTypeAsync(dto);
            return ResponseFactory.Created(newReportType);
        }


        /// <summary>
        /// Update A Report Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("type/{id:int}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> UpdateTypeAsync(int id, ReportTypeWriteDTO dto)
        {
            var updatedTypeReport = await _reportService.UpdateTypeAsync(id, dto);
            return ResponseFactory.Ok(updatedTypeReport);
        }

        /// <summary>
        /// Delete A Report Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("type/{id:int}")]
        public async Task<IActionResult> DeleteTypeAsync(int id)
        {
            await _reportService.DeleteTypeAsync(id);
            return ResponseFactory.NoContent();
        }
    }
}

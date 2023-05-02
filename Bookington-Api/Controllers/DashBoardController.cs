using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.DashBoard;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Dashboard API
    /// </summary>
    [Route("dashboard")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashboardService;
        /// <summary>
        /// 
        /// </summary>
        public DashBoardController(IDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Admin Dash Board
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("admin")]
        [RoleAuthorize(AccountRole.admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetAdminDashBoard([FromQuery] DashBoardQuery query)
        {
            var dashboardInfo = await _dashboardService.GetAdminDashBoard(query);
            return ResponseFactory.Ok(dashboardInfo);
        }

        /// <summary>
        /// Owner Dash Board
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("owner")]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetOwnerDashBoard([FromQuery] string ownerId,[FromQuery] DashBoardQuery query)
        {
            var dashboardInfo = await _dashboardService.GetOwnerDashBoard(ownerId,query);
            return ResponseFactory.Ok(dashboardInfo);
        }
    }
}

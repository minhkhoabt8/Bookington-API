using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.SubCourtSlot;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    /// 
    [Route("slots")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly int DEFAULT_SLOT_DURATION = 30;

        private readonly ISlotService _slotService;

        /// <summary>        
        /// </summary>
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        /// <summary>
        /// Get All Default Slots Of System
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [RoleAuthorize(AccountRole.admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetAllSlots()
        {
            var slots = await _slotService.GetAllDefaultSlotsAsync();
            return ResponseFactory.Ok(slots);
        }

        /// <summary>
        /// Generate Default Slots
        /// </summary>
        /// <returns></returns>
        [HttpPost("generate/system")]
        [RoleAuthorize(AccountRole.admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GenerateDefaultSlots()
        {
            await _slotService.GenerateDefaultSlots(DEFAULT_SLOT_DURATION);
            return ResponseFactory.Ok("Slots (" + DEFAULT_SLOT_DURATION + " minutes per slot) are generated successfully!");
        }

        /// <summary>
        /// Generate Default Slots For Sub Court
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("generate/subcourt")]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GenerateDefaultSlotsForSubCourt(DefaultSubCourtSlotWriteDTO dto)
        {
            var result = await _slotService.GenerateDefaultSlotsForSubCourt(dto);
            return ResponseFactory.Ok(result);
        }

        /// <summary>
        /// Get Schedule Of A Sub Court
        /// </summary>
        /// <param name="subCourtId"></param>
        /// <returns></returns>
        [HttpGet("schedule/{subCourtId}")]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetScheduleOfASubCourt(string subCourtId)
        {
            var schedule = await _slotService.GetScheduleOfASubCourt(subCourtId);
            return ResponseFactory.Ok(schedule);
        }
    }
}

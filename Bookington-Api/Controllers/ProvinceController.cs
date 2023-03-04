using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.DTOs.Province;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Province Controller
    /// </summary>
    [Route("province")]
    [Authorize(Roles = "user,admin,owner")]
    [ApiController]
    public class ProvinceController : Controller
    {
        private readonly IProvinceService _provinceService;

        /// <summary>        
        /// </summary>        
        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        /// <summary>
        /// Get All Province
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProvinceReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var provinces = await _provinceService.GetAllAsync();
            return ResponseFactory.Ok(provinces);
        }

        /// <summary>
        /// Create a new province
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateAsync(ProvinceWriteDTO dto)
        {
            var createdTag = await _provinceService.CreateAsync(dto);
            return ResponseFactory.Created(createdTag);
        }

        /// <summary>
        /// Sync Province
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> SyncProvince()
        {
            await _provinceService.SyncProvince();
            return ResponseFactory.NoContent();
        }
    }
}

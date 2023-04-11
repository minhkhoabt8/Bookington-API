using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.District;
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
    [Route("districts")]
    [ApiController]
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;


        /// <summary>        
        /// </summary>        
        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        /// <summary>
        /// Get All District
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DistrictReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var districts = await _districtService.GetAllAsync();
            return ResponseFactory.Ok(districts);
        }
        /// <summary>
        /// Create a new district
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateAsync(DistrictWriteDTO dto)
        {
            var createdTag = await _districtService.CreateAsync(dto);
            return ResponseFactory.Created(createdTag);
        }

        /// <summary>
        /// Sync District
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> SyncProvince()
        {
            await _districtService.SyncDistrict();
            return ResponseFactory.NoContent();
        }

        /// <summary>
        /// Get Districts By Province Id
        /// </summary>
        ///<param name="provinceId"></param>
        /// <returns></returns>
        [HttpGet("province")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DistrictReadDTO>))]
        public async Task<IActionResult> GetDistrictsByProvinceIdAsync([FromQuery]string id)
        {

            var districts = await _districtService.GetDistrictsByProvinceIdAsync(id);

            return ResponseFactory.Ok(districts);
        }
    }
}

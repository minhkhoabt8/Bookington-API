using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Role Controller
    /// </summary>
    [ApiController]
    [RoleAuthorize(AccountRole.admin)]
    [Route("auth/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        /// <summary>        
        /// </summary>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<IEnumerable<RoleReadDTO>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> GetAllRoles()
        {
            var roleItems = await _roleService.GetAllAsync();

            return ResponseFactory.Ok(roleItems);
        }
        /// <summary>
        /// Get role detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<RoleReadDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        public async Task<IActionResult> GetRole(string id)
        {
            var roleItems = await _roleService.GetByIdAsync(id);

            return ResponseFactory.Ok(roleItems);
            
        }
        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="writeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<RoleReadDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> CreateRole(RoleWriteDTO writeDTO)
        {
            var createdRoleDTO = await _roleService.CreateAsync(writeDTO);

            return ResponseFactory.CreatedAt(nameof(GetRole),
            nameof(RoleController),
            new { id = createdRoleDTO.Id },
            createdRoleDTO);
        }
        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="writeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<RoleReadDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> UpdateRole(int id, RoleWriteDTO writeDTO)
        {
            var roleDTO = await _roleService.UpdateAsync(id, writeDTO);

            return ResponseFactory.Ok(roleDTO);
        }
        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteAsync(id);

            return ResponseFactory.NoContent();
        }
        
    }
}

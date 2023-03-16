using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.CheckOut;
using Bookington.Infrastructure.DTOs.Order;
using Bookington.Infrastructure.DTOs.Province;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>    
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>        
        /// </summary>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get All Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _orderService.GetAllAsync();
            return ResponseFactory.Ok(orders);
        }

        /// <summary>
        /// Get Order Details By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>                
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderReadDTO>))]
        public async Task<IActionResult> GetDetailsByIdAsync(string id)
        {
            // TODO: JSON Needs To Be Fixed Later
            var order = await _orderService.GetByIdAsync(id);
            return ResponseFactory.Ok(order);
        }

        /// <summary>
        /// Check Out
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>                
        [HttpPost("checkout")]
        [RoleAuthorize(AccountRole.user)]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> CheckOutAsync(CheckOutWriteDTO dto)
        {            
            var result = await _orderService.CheckOutAsync(dto);
            return ResponseFactory.Ok("Check out successfully! " + result);
        }
    }
}

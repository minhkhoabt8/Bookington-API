﻿using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.CheckOut;
using Bookington.Infrastructure.DTOs.Order;
using Bookington.Infrastructure.DTOs.Province;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// Get All Orders Of User
        /// </summary>
        ///<param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderReadDTO>))]
        public async Task<IActionResult> GetAllOrderOfUserAsync([FromQuery] OrderQuery query)
        {
            var orders = await _orderService.GetAllOrderOfUserAsync(query);
            return ResponseFactory.PaginatedOk(orders);
        }


        /// <summary>
        /// Get Order Details By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>                
        [HttpGet("{id}")]
        [RoleAuthorize(AccountRole.owner, AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderReadDTO))]
        public async Task<IActionResult> GetDetailsByIdAsync(string id)
        {            
            var order = await _orderService.GetByIdAsync(id);
            return ResponseFactory.Ok(order);
        }

        /// <summary>
        /// Check Out
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>                
        [HttpPost("checkout")]
        [RoleAuthorize(AccountRole.customer)]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> CheckOutAsync(CheckOutWriteDTO dto)
        {            
            var result = await _orderService.CheckOutAsync(dto);
            return ResponseFactory.Ok(result, "Check out successfully!");
        }

        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>                
        [HttpPost("cancel")]
        [RoleAuthorize(AccountRole.customer)]
        [ServiceFilter(typeof(AutoValidateModelState))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderReadDTO))]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var result = await _orderService.CancelOrderAsync(orderId);

            return ResponseFactory.Ok(result);
        }

    }
}

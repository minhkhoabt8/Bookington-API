﻿using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Auth Controller
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;
        /// <summary>        
        /// </summary>
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        ///Login
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> Login(AccountLoginInputDTO input)
        {
            var result = await _accountService.LoginWithPhoneNumber(input);
            return ResponseFactory.Ok(result);
        }

    }
}
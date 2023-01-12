﻿using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    [Route("bookington/courts")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtService _courtService;

        public CourtController(ICourtService courtService)
        {
            _courtService = courtService;
        }

        /// <summary>
        /// Get All Court
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourtReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var tags = await _courtService.GetAllAsync();
            return ResponseFactory.Ok(tags);
        }

        /// <summary>
        /// Get a Court details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourtReadDTO))]
        public async Task<IActionResult> GetDetailsAsync(string id)
        {
            var tag = await _courtService.GetByIdAsync(id);
            return ResponseFactory.Ok(tag);
        }

        /// <summary>
        /// Create a new court
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> CreateAsync(CourtWriteDTO dto)
        {
            var createdTag = await _courtService.CreateAsync(dto);
            return ResponseFactory.Created(createdTag);
        }


        /// <summary>
        /// Update a court
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> UpdateAsync(int id, CourtWriteDTO dto)
        {
            var updatedTag = await _courtService.UpdateAsync(id, dto);
            return ResponseFactory.Ok(updatedTag);
        }

        /// <summary>
        /// Delete a court
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _courtService.DeleteAsync(id);
            return ResponseFactory.NoContent();
        }

    }
}
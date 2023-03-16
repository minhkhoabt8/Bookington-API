﻿using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.Slot;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Booking Controller
    /// </summary>
    [Route("bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ISubCourtService _subCourtService;
        private readonly ISlotService _slotService;

        /// <summary>        
        /// </summary>
        public BookingController(IBookingService bookingService, ISubCourtService subCourtService, ISlotService slotService)
        {
            _bookingService = bookingService;
            _subCourtService = subCourtService;
            _slotService = slotService;
        }

        /// <summary>
        /// Get All Bookings
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookingReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookings = await _bookingService.GetAllAsync();
            return ResponseFactory.Ok(bookings);
        }

        /// <summary>
        /// Get a Booking details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingReadDTO))]
        public async Task<IActionResult> GetDetailsAsync(string id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            return ResponseFactory.Ok(booking);
        }

        /// <summary>
        /// Create a new booking(s) order
        /// </summary>        
        /// <param name="dtos"></param>
        /// <returns></returns>        
        [HttpPost]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> CreateBookingsAsync(List<BookingWriteDTO> dtos)
        {
            var newBookings = await _bookingService.CreateBookingsAsync(dtos);
            return ResponseFactory.Created(newBookings);
        }


        /// <summary>
        /// Update a Booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> UpdateAsync(string id, BookingWriteDTO dto)
        {
            var updatedTag = await _bookingService.UpdateAsync(id, dto);
            return ResponseFactory.Ok(updatedTag);
        }

        /// <summary>
        /// Delete a Booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _bookingService.DeleteAsync(id);
            return ResponseFactory.NoContent();
        }

        /// <summary>
        /// Get Booking History Of A Court
        /// </summary>
        /// <param name="courtId"></param>
        /// <returns></returns>
        [HttpGet("{courtId}")]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourtBookingHistoryReadDTO>))]
        public async Task<IActionResult> GetBookingHistoryOfCourt(string courtId)
        {
            var bookings = await _bookingService.GetBookingsOfCourt(courtId);
            return ResponseFactory.Ok(bookings);
        }

        /// <summary>
        /// Get Incoming Matches From Booking Of A User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("incomingMatch")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookingReadDTO>))]
        public async Task<IActionResult> GetIncomingMatchesFromBookingOfUser([Required] string userId)
        {
            var bookings = await _bookingService.GetIncomingMatchesFromBookingOfUser(userId);
            return ResponseFactory.Ok(bookings);
        }

        /// <summary>
        /// Get Available Sub Courts For Booking
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("subcourts/available")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<IEnumerable<SubCourtForBookingReadDTO>>))]
        public async Task<IActionResult> GetAvailableSubCourtsForBooking([FromBody] SubCourtQueryForBooking dto)
        {
            var subCourts = await _subCourtService.GetSubCourtsForBooking(dto);
            return ResponseFactory.Ok(subCourts);
        }

        /// <summary>
        /// Get Available Slots For Booking
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("slots/available")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<SlotsForBookingReadDTO>))]
        public async Task<IActionResult> GetAvailableSlotsForBooking([FromBody] SlotQueryForBooking dto)
        {
            var result = await _slotService.GetAvailableSlotsForBooking(dto);
            return ResponseFactory.Ok(result);
        }
    }
}

using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    /// <summary>
    /// Booking History Controller
    /// </summary>
    [Route("booking-history")]
    [ApiController]
    public class BookingHistoryController : ControllerBase
    {
        private readonly IBookingService _bookingService;        

        /// <summary>        
        /// </summary>
        public BookingHistoryController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Get Booking History Of A Court
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("courts")]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<CourtBookingHistoryReadDTO>))]
        public async Task<IActionResult> GetBookingHistoryOfCourt([FromQuery] BookingHistoryQuery query)
        {
            var bookings = await _bookingService.GetBookingsOfCourt(query);
            return ResponseFactory.PaginatedOk(bookings);
        }

        /// <summary>
        /// Get Booking History Of A SubCourt
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("sub-courts")]
        [RoleAuthorize(AccountRole.owner)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<CourtBookingHistoryReadDTO>))]
        public async Task<IActionResult> GetBookingHistoryOfCourt([FromQuery] GetBookingsOfSubCourtPaginatedQuery query)
        {
            var bookings = await _bookingService.GetBookingsOfASubCourtAsync(query);

            return ResponseFactory.PaginatedOk(bookings);
        }


        /// <summary>
        /// Get Incoming Bookings Of A Customer
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("customers/incoming")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<BookingReadDTO>))]
        public async Task<IActionResult> GetIncomingBookingsOfCustomer([FromQuery] IncomingBookingQuery query)
        {
            var bookings = await _bookingService.GetIncomingBookingsOfCustomer(query);
            return ResponseFactory.PaginatedOk(bookings);
        }

        /// <summary>
        /// Get Finished Bookings Of A Customer
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("customers/finished")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<BookingReadDTO>))]
        public async Task<IActionResult> GetFinishedBookingsOfCustomer([FromQuery] FinishedBookingQuery query)
        {
            var bookings = await _bookingService.GetFinishedBookingsOfCustomer(query);
            return ResponseFactory.PaginatedOk(bookings);
        }
    }
}

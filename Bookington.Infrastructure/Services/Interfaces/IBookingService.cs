using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.IncomingBooking;
using Bookington.Infrastructure.DTOs.IncomingMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingReadDTO>> GetAllAsync();
        //Task<BookingReadDTO> CreateAsync(BookingWriteDTO dto);
        Task<IEnumerable<BookingReadDTO>> CreateBookingsAsync(IEnumerable<BookingWriteDTO> bookings);
        Task<BookingReadDTO> UpdateAsync(string id, BookingWriteDTO dto);
        Task DeleteAsync(string id);
        Task<BookingReadDTO> GetByIdAsync(string id);
        Task<PaginatedResponse<CourtBookingHistoryReadDTO>> GetBookingsOfCourt(BookingHistoryQuery query);
        Task<PaginatedResponse<IncomingBookingReadDTO>> GetIncomingBookingsOfCustomer(IncomingBookingQuery query);
        Task<PaginatedResponse<FinishedBookingReadDTO>> GetFinishedBookingsOfCustomer(FinishedBookingQuery query);
    }
}

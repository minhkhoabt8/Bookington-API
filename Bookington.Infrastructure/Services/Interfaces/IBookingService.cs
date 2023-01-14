using Bookington.Infrastructure.DTOs.Booking;
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
        Task<BookingReadDTO> CreateAsync(BookingWriteDTO dto);
        Task<BookingReadDTO> UpdateAsync(string id, BookingWriteDTO dto);
        Task DeleteAsync(string id);
        Task<BookingReadDTO> GetByIdAsync(string id);
        Task<IEnumerable<CourtBookingHistoryReadDTO>> GetBookingsOfCourt(string courtId);
    }
}

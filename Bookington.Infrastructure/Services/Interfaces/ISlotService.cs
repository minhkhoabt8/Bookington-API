using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ISlotService
    {
        Task<IEnumerable<SlotReadDTO>> GetAllAsync();
        Task<SlotReadDTO> CreateAsync(SlotWriteDTO dto);
        Task<SlotReadDTO> UpdateAsync(int id, SlotWriteDTO dto);
        Task DeleteAsync(int id);
        Task<SlotReadDTO> GetByIdAsync(string id);
        Task<SlotsForBookingReadDTO> GetAvailableSlotsForBooking(SlotQueryForBooking dto);
    }
}

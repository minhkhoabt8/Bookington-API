using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Slot;
using Bookington.Infrastructure.DTOs.SubCourtSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ISlotService
    {
        Task GenerateDefaultSlots(int slotDuration);
        Task<string> GenerateDefaultSlotsForSubCourt(DefaultSubCourtSlotWriteDTO dto);
        Task<IEnumerable<SlotReadDTO>> GetAllDefaultSlotsAsync();
        Task<IEnumerable<SubCourtSlotScheduleReadDTO>> GetScheduleOfASubCourt(string subCourtId);
        Task<SlotsForBookingReadDTO> GetAvailableSlotsForBooking(SlotQueryForBooking dto);        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Slot;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ISlotRepository :
        IGetAllAsync<Slot>,
        IAddAsync<Slot>,
        IUpdate<Slot>,
        IFindAsync<Slot>,
        IDelete<Slot>
    {
        Task<IEnumerable<Slot>> GetAllDefaultSlotsAsync();
        Task<IEnumerable<SubCourtSlot>> GetAvailableSlotsForBooking(SlotQueryForBooking dto);
        Task<bool> IsSlotBooked(string slotId, DateTime playDate);
        Task<double> GetTheLowestSlotPriceOfACourt(string courtId, bool trackChanges = false);
    }
}

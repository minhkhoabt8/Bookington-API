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
        Task<Account> GetCourtOwnerBySlotId(string slotId, bool trackChanges = false);
        Task<string> GetCourtNameBySlotId(string slotId, bool trackChanges = false);
        Task<IEnumerable<Slot>> GetAvailableSlotsForBooking(SlotQueryForBooking dto, bool trackChanges = false);
        Task<bool> IsSlotBooked(string slotId, DateTime playDate, bool trackChanges = false);
    }
}

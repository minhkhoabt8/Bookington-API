using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ISubCourtSlotRepository :
        IFindAsync<SubCourtSlot>,
        IGetAllAsync<SubCourtSlot>,
        IAddAsync<SubCourtSlot>,
        IUpdate<SubCourtSlot>,        
        IDelete<SubCourtSlot>       
    {
        Task<bool> SubCourtHasSlot(string subCourtId);
        Task FlushSlotsOfSubCourt(string subCourtId);
        public Task<SubCourtSlot> FindAsync(string subCourtId, string slotId, bool trackChanges = true);
        public Task<IEnumerable<SubCourtSlot>> GetScheduleOfASubCourt(string subCourtId);
        public Task<IEnumerable<SubCourtSlot?>> GetListSubCourtSlotsBySubCourtId(string subCourtId);
    }
}

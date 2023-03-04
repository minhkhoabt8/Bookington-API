using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.SubCourt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ISubCourtRepository :
        IGetAllAsync<SubCourt>,
        IAddAsync<SubCourt>,
        IUpdate<SubCourt>,        
        IFindAsync<SubCourt>
    {
        Task<IEnumerable<SubCourt>> GetAvailableSubCourtsByCourtId(string courtId, bool trackChanges = false);
        Task<IEnumerable<SubCourt>> GetSubCourtsForBooking(SubCourtQueryForBooking dto, bool trackChanges = false);
        //Task<IEnumerable<SubCourt>> GetUnavailableSubCourtsForBooking(SubCourtQueryForBooking dto, bool trackChanges = false);
    }
}

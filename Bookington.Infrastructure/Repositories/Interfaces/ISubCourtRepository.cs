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
        IQueryAsync<SubCourt,SubCourtQuery>,
        IGetAllAsync<SubCourt>,
        IAddAsync<SubCourt>,
        IUpdate<SubCourt>,        
        IFindAsync<SubCourt>
    {
        Task<int> GetNumberOfSubCourts(string courtId, bool trackChanges = false);
        Task<IEnumerable<SubCourt>> GetAvailableSubCourtsByCourtId(string courtId, bool trackChanges = false);
        Task<IEnumerable<SubCourt>> GetSubCourtsForBooking(SubCourtQueryForBooking dto);
        Task<Account> GetCourtOwnerBySubCourtId(string subCourtId);
        Task<string> GetCourtNameBySubCourtId(string subCourtId);
        Task<IEnumerable<SubCourt>> GetSubCourtsOfOwner(string ownerId);

        Task<IEnumerable<SubCourt>> QuerySubCourtOfCourt(SubCourtQuery query, bool trackChanges = false);
    }
}

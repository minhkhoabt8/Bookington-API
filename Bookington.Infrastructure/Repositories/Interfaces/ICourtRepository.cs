using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Court;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ICourtRepository : 
        IFindAsync<Court>,
        IGetAllAsync<Court>,
        IAddAsync<Court>,
        IUpdate<Court>,
        IDelete<Court>,
        IQueryAsync<Court,CourtItemQuery>
    {
        Task<IEnumerable<Court?>> GetAllCourtByOwnerIdAsync(string ownerId);
    }
}

using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IBanRepository :    
        IGetAllAsync<Ban>,
        IAddAsync<Ban>,
        IUpdate<Ban>,
        IFindAsync<Ban>,
        IDelete<Ban>
    {

        Task<Ban?> FindCourtBanByCourtIdAsync(string courtId);
        Task<Ban?> FindUserBanByUserIdAsync(string userId);

        Task<IEnumerable<Ban?>> GetAllExpiredCourtBanAsync();

        Task<IEnumerable<Ban?>> GetAllExpiredUserBanAsync();

    }
}

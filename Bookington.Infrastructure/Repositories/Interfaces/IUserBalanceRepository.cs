using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IUserBalanceRepository :
        IGetAllAsync<UserBalance>,
        IAddAsync<UserBalance>,
        IUpdate<UserBalance>,
        IFindAsync<UserBalance>
    {
        Task<UserBalance?> FindByAccountIdAsync(string accountId, bool trackChanges = false);
        Task<UserBalance?> FindAdminAccountBalance();
        Task<IEnumerable<UserBalance?>> GetUserBalancesOfOwnerToPayout();
    }
}

using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Ban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IBanServices
    {
        Task<BanReadDTO?> FindCourtBanByCourtIdAsync(string courtId);

        Task<BanReadDTO?> FindUserBanByUserIdAsync(string userId);

        Task UnBanCourtCronJobAsync();

        Task UnBanUserCronJobAsync();
        
        //shouldn't do delete
    }
}

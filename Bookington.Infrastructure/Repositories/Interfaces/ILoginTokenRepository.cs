using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ILoginTokenRepository : IAddAsync<LoginToken>
    {
        Task<LoginToken?> FindByTokenAsync(string token);
        Task<LoginToken?> FindByTokenIncludeAccountAsync(string? token);
    }
}

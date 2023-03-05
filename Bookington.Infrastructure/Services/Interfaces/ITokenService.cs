using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(Account account);

        LoginToken GenerateRefreshToken(Account account);
    }
}

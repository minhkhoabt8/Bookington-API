using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class LoginTokenRepository : GenericRepository<LoginToken, BookingtonDbContext>, ILoginTokenRepository
    {
        public LoginTokenRepository(BookingtonDbContext context) : base(context)
        {
        }
        public Task<LoginToken?> FindByTokenAsync(string token)
        {
            return _context.LoginTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public Task<LoginToken?> FindByTokenIncludeAccountAsync(string? token)
        {
            return _context.LoginTokens.Include(lt=>lt.RefAccountNavigation).ThenInclude(acc => acc.Role)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

    }
}

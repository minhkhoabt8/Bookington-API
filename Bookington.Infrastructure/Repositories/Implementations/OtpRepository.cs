using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class OtpRepository : GenericRepository<AccountOtp, BookingtonDbContext>, IOtpRepository
    {
        public OtpRepository(BookingtonDbContext context) : base(context)
        {
        }
        public async Task<AccountOtp?> VerifyAccountAsync(string phoneNumber, string otp)
        {
            return await _context.AccountOtps.FirstOrDefaultAsync(a=>a.Phone==phoneNumber && a.OtpCode==otp && a.IsConfirmed == false);
        }

        public async Task<AccountOtp?> FindAccountOtpByPhone(string phone)
        {
            return await _context.AccountOtps.FirstOrDefaultAsync(a => a.Phone == phone && a.IsConfirmed == false);
        }
    }
}

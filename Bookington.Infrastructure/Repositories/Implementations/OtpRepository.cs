using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class OtpRepository : GenericRepository<AccountOtp, BookingtonDbContext>, IOtpRepository
    {
        public OtpRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

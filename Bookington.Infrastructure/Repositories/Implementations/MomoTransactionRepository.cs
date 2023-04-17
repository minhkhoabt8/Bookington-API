using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class MomoTransactionRepository : GenericRepository<MomoTransaction, BookingtonDbContext>, IMomoTransactionRepository
    {
        public MomoTransactionRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

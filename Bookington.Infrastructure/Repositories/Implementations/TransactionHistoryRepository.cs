using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class TransactionHistoryRepository : GenericRepository<TransactionHistory, BookingtonDbContext>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

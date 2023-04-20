using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class TransactionRepository : GenericRepository<Transaction, BookingtonDbContext>, ITransactionRepository
    {        
        public TransactionRepository(BookingtonDbContext context) : base(context)
        {
        }
        
        public Task<IEnumerable<Transaction>> GetTransactionHistoryOfUser(string userId)
        {
            var dbSet = _context.Transactions.Include(th => th.RefFromNavigation).Include(th => th.RefToNavigation).Include(th => th.Orders).ToList();

            var trans = dbSet.Where(sc => sc.RefFrom == userId || sc.RefTo == userId).OrderByDescending(sc => sc.CreateAt).AsEnumerable();            

            return Task.FromResult(trans);            
        }

        public async Task<Transaction?> GetTransactionHstoryByMomoOrderId(string momoOrderId)
        {
            return await _context.Transactions
                    .Include(t => t.RefMomoTransactionNavigation)
                    .FirstOrDefaultAsync(t => t.RefMomoTransaction == momoOrderId);
        }
    }
}

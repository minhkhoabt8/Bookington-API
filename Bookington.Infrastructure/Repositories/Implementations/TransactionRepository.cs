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
        
        public Task<IEnumerable<Transaction>> GetTransactionHistoryOfCustomer(string userId)
        {
            var dbSet = _context.Transactions.Include(th => th.RefFromNavigation).Include(th => th.RefToNavigation).ToList();

            var trans = dbSet.Where(sc => sc.RefFrom == userId).OrderByDescending(sc => sc.CreateAt).AsEnumerable();            

            return Task.FromResult(trans);            
        }
    }
}

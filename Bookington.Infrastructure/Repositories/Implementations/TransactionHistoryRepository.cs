using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class TransactionHistoryRepository : GenericRepository<TransactionHistory, BookingtonDbContext>, ITransactionHistoryRepository
    {
        // 10 RECORDS FOR EACH PAGE
        private int NUM_OF_RECORDS_FOR_A_PAGE = 10;

        public TransactionHistoryRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<TransactionHistory>> GetTransactionHistoryOfCustomer(string userId, int page, bool trackChanges = false)
        {
            IQueryable<TransactionHistory> dbSet = _context.Set<TransactionHistory>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }

            var trans = dbSet.Where(sc => sc.RefFrom == userId).OrderByDescending(sc => sc.CreateAt).AsEnumerable();

            var resultTrans = trans;

            if (trans.Count() >= NUM_OF_RECORDS_FOR_A_PAGE)
            {
                resultTrans = new List<TransactionHistory>();

                for (int i = 0 + NUM_OF_RECORDS_FOR_A_PAGE * (page - 1); i < NUM_OF_RECORDS_FOR_A_PAGE * page; i++)
                {
                    var t = trans.ElementAt(i);

                    if (t == null) break;
                        
                    resultTrans.Append(trans.ElementAt(i));
                }
            }

            return Task.FromResult(resultTrans.AsEnumerable());
        }
    }
}

using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ITransactionHistoryRepository :
        IGetAllAsync<TransactionHistory>,
        IFindAsync<TransactionHistory>,
        IAddAsync<TransactionHistory>,
        IUpdate<TransactionHistory>,        
        IDelete<TransactionHistory>
    {
        Task<IEnumerable<TransactionHistory>> GetTransactionHistoryOfCustomer(string userId, int page);
    }
}

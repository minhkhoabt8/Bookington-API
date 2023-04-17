using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ITransactionRepository :
        IGetAllAsync<Transaction>,
        IFindAsync<Transaction>,
        IAddAsync<Transaction>,
        IUpdate<Transaction>,        
        IDelete<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionHistoryOfCustomer(string userId);
        Task<IEnumerable<Transaction>> GetTransactionHistoryOfOwner(string ownerId);
        Task<Transaction?>GetTransactionHstoryByMomoOrderId(string momoOrderId);
    }
}

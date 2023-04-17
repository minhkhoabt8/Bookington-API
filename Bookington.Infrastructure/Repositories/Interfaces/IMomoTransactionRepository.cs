using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IMomoTransactionRepository: IGetAllAsync<MomoTransaction>,
        IAddAsync<MomoTransaction>,
        IUpdate<MomoTransaction>,
        IFindAsync<MomoTransaction>,
        IDelete<MomoTransaction>
        //IQueryAsync<MomoTransaction, AccountQuery>
    {
    }
}

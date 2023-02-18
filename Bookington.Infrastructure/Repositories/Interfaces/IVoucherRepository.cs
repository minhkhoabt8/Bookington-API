using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IVoucherRepository :
        IGetAllAsync<Voucher>,
        IAddAsync<Voucher>,
        IUpdate<Voucher>,
        IFindAsync<Voucher>
    {
        Task<Voucher> FindByCode(string voucherCode, bool trackChanges = false);
    }    
}

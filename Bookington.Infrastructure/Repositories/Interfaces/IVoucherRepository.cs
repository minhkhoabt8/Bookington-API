using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Voucher;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IVoucherRepository :
        IGetAllAsync<Voucher>,
        IAddAsync<Voucher>,
        IUpdate<Voucher>,
        IFindAsync<Voucher>,
        IDelete<Voucher>
    {
        Task<Voucher> FindByCode(string voucherCode, bool trackChanges = false);
        Task<IEnumerable<Voucher>> GetAllVoucherOfACourtAsync(string courtId);
        Task<Voucher?> GetVoucherByCodeOfCourtAsync(string courtId, string voucherCode);

        Task<IEnumerable<Voucher?>> GetAllVoucherOfACourtByOwnerIdAsync(string ownerId);
    }    
}

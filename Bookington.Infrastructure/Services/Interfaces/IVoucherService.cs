using Bookington.Infrastructure.DTOs.Comment;
using Bookington.Infrastructure.DTOs.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<VoucherReadDTO> CreateAsync(VoucherWriteDTO dto);
        Task<VoucherReadDTO> UpdateAsync(string id, VoucherWriteDTO dto);
        Task DeleteAsync(string id);
        Task<IEnumerable<VoucherReadDTO>> GetAllVoucherOfACourtAsync(string courtId);

    }
}

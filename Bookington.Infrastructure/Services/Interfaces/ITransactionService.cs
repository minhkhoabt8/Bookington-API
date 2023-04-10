using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.TransactionHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionHistoryReadDTO>> GetAllAsync();
        Task<TransactionHistoryReadDTO> CreateAsync(TransactionHistoryWriteDTO dto);
        Task<TransactionHistoryReadDTO> UpdateAsync(string id, TransactionHistoryWriteDTO dto);
        Task DeleteAsync(string id);
        Task<TransactionHistoryReadDTO> GetByIdAsync(string id);
        Task<string> TransferAsync(double amount, string refTo, string transferReason);
        Task<PaginatedResponse<TransactionHistoryReadDTO>> GetSelfTransactionHistory(TransactionHistoryQuery query);
    }
}

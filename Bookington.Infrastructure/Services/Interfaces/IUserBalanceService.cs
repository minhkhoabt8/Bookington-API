using Bookington.Infrastructure.DTOs.UserBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IUserBalanceService
    {
        Task<UserBalanceReadDTO> CreateAsync(UserBalanceWriteDTO dto);
        Task<IEnumerable<UserBalanceReadDTO>> GetAllAsync();
        Task<UserBalanceReadDTO> GetByIdAsync(string id);
        Task<UserBalanceReadDTO> GetSelfBalance();
        Task<UserBalanceReadDTO> AddBalanceAsync(UserBalanceWriteDTO dto);
    }
}

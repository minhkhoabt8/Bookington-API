using Bookington.Infrastructure.DTOs.Account;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountReadDTO>> GetAllAsync();
        Task<AccountReadDTO> CreateAsync(AccountWriteDTO dto);
    }
}

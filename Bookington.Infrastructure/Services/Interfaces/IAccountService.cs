using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Role;
using Microsoft.AspNetCore.Mvc;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountReadDTO>> GetAllAsync();

        Task<AccountReadDTO> CreateAsync(AccountWriteDTO dto);

        Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto);

        Task<AccountReadDTO> UpdateAsync(string id, AccountWriteDTO dto);

        Task DeleteAsync(string id);

        Task<AccountReadDTO> GetByIdAsync(string id);


        Task VerifyAccount(string phoneNumber, string otp);

        Task<PaginatedResponse<AccountReadDTO>> QueryAccountsAsync(AccountQuery query);
    }
}

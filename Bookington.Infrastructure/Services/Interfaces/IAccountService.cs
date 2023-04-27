using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.File;
using Bookington.Infrastructure.DTOs.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountReadDTO>> GetAllAsync();

        Task<AccountReadDTO> CreateAsync(AccountWriteDTO dto);

        Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto);

        Task<AccountLoginOutputDTO> LoginWithRefreshTokenAsync(string? token);

        Task<AccountReadDTO> UpdateAsync(string id, AccountUpdateDTO dto);

        Task DeleteAsync(string id);

        Task<AccountReadDTO> GetByIdAsync(string id);

        Task VerifyAccount(string phoneNumber, string otp);

        Task<PaginatedResponse<AccountReadDTO>> QueryAccountsAsync(AccountQuery query);

        Task<AccountProfileReadDTO> GetProfileAsync();

        Task<AccountProfileReadDTO> GetProfileByIdAsync(string accountId);

        Task AssignRoleToUserAsync(string userId, string roleId);

        Task ReSendVerifyOtp(string phone);

        Task ChangePasswordAsync(ChangePasswordDTO dto);

        Task UpdateAccountAvatar(FileUploadDTO dto);

        Task<AccountReadDTO> VerifyPhoneNumber(string phoneNumber);  

        Task<AccountReadDTO> UpdatePassword(string phones, string password);
    }
}

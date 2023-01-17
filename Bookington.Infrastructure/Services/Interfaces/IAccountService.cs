using Bookington.Infrastructure.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountReadDTO>> GetAllAsync();

        Task<AccountReadDTO> CreateAsync(AccountWriteDTO dto);

        Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto);

        Task VerifyAccount(string phoneNumber, string otp);
    }
}

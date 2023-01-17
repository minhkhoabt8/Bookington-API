using Azure;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository : 
        IGetAllAsync<Account>,
        IAddAsync<Account>,
        IUpdate<Account>,
        IFindAsync<Account>,
        IDelete<Account>
    {
        Task<Account?> FindAccountByPhoneNumberAsync(string phoneNumber);
        Task<Account?> LoginByPhoneAsync(AccountLoginInputDTO account);
    }
}

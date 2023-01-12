using Azure;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository : 
        IGetAllAsync<Account>,
        IAddAsync<Account>,
        IUpdate<Account>,
        IFindAsync<Account>
    {
        Task<Account?> FindAccountByPhoneNumber(string phoneNumber);
        Task<Account?> GetUserUsernameAndPass(AccountLoginInputDTO account);
    }
}

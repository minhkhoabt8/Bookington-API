
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.Court;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository : 
        IGetAllAsync<Account>,
        IAddAsync<Account>,
        IUpdate<Account>,
        IFindAsync<Account>,
        IDelete<Account>,
        IQueryAsync<Account,AccountQuery>
    {
        Task<Account?> FindAccountByPhoneNumberAsync(string phoneNumber);
        Task<Account?> LoginByPhoneAsync(AccountLoginInputDTO account);
        Task<IEnumerable<Account>> QueryAsync(AccountQuery query, bool trackChanges = false);
    }
}

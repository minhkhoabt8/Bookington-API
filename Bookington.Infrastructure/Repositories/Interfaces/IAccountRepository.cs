using Azure;
using Bookington.Core.Entities;
namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository : 
        IGetAllAsync<Account>,
        IAddAsync<Account>,
        IUpdate<Account>,
        IFindAsync<Account>
    {
        Task<Account?> FindAccountByPhoneNumber(string phoneNumber);
    }
}

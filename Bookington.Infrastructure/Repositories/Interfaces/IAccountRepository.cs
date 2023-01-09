using Bookington.Core.Entities;
namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository : 
        IGetAllAsync<Account>
    {
    }
}

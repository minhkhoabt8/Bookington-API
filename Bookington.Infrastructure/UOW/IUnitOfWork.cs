using Bookington.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.UOW
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        Task<int> CommitAsync();
    }
}

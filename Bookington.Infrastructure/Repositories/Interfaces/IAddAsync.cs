using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IAddAsync<in T> where T : class
    {
        Task AddAsync(T obj);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IUserContextService
    {
        Guid? AccountID { get; }
        string? FullName { get; }
        string? Phone { get; }
    }
}

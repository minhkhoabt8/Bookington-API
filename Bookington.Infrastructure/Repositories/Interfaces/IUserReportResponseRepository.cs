using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IUserReportResponseRepository :
        IGetAllAsync<UserReportResponse>,
        IAddAsync<UserReportResponse>,
        IUpdate<UserReportResponse>,
        IFindAsync<UserReportResponse>,
        IDelete<UserReportResponse>
    {
    }
}

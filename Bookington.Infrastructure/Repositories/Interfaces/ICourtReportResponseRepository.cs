using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ICourtReportResponseRepository :        
        IAddAsync<CourtReportResponse>,
        IUpdate<CourtReportResponse>,
        IFindAsync<CourtReportResponse>,
        IDelete<CourtReportResponse>
    {
    }
}

﻿using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ICourtReportRepository :
        IGetAllAsync<CourtReport>,
        IFindAsync<CourtReport>,
        IAddAsync<CourtReport>,        
        IUpdate<CourtReport>,
        IDelete<CourtReport>
    {
    }
}

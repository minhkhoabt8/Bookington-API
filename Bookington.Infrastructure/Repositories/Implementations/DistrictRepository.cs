﻿using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class DistrictRepository : GenericRepository<District, BookingtonDbContext>, IDistrictRepository
    {
        public DistrictRepository(BookingtonDbContext context) : base(context)
        {

        }
    }
}

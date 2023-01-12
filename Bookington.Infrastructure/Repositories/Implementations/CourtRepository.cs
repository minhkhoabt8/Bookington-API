﻿using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;


namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class CourtRepository : GenericRepository<Court, BookingtonDbContext>, ICourtRepository
    {
        public CourtRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

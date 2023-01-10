using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using SharedLib.Infrastructure.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class CourtRepository : GenericRepository<Court, BookingtonDbContext>, ICourtRepository
    {
        public CourtRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

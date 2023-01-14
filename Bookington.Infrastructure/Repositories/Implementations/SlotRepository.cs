using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Core.Data;
using Bookington.Infrastructure.Repositories.Interfaces;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class SlotRepository : GenericRepository<Slot, BookingtonDbContext>, ISlotRepository
    {
        public SlotRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

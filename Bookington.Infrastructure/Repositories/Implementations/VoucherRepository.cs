using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Infrastructure.Repositories.Interfaces;
using Bookington.Core.Entities;
using Bookington.Core.Data;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class VoucherRepository : GenericRepository<Voucher, BookingtonDbContext>, IVoucherRepository
    {
        public VoucherRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}

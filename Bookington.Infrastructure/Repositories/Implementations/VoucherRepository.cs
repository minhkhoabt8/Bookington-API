using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Infrastructure.Repositories.Interfaces;
using Bookington.Core.Entities;
using Bookington.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class VoucherRepository : GenericRepository<Voucher, BookingtonDbContext>, IVoucherRepository
    {
        public VoucherRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<Voucher> FindByCode(string voucherCode, bool trackChanges = false)
        {
            IQueryable<Voucher> dbSet = _context.Set<Voucher>();
            if (trackChanges == false)
            {
                dbSet = dbSet.AsNoTracking();
            }                        

            return Task.FromResult(dbSet.FirstOrDefault(v => v.VoucherCode == voucherCode))!;
        }
    }
}

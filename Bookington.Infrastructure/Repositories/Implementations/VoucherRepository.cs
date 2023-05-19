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

        public Task<IEnumerable<Voucher>> GetAllVoucherOfACourtAsync(string courtId)
        {
            var vouchers = _context.Vouchers.Where(v => v.RefCourt == courtId);

            return Task.FromResult(vouchers.AsEnumerable());
        }

        public async Task<Voucher?> GetVoucherByCodeOfCourtAsync(string courtId, string voucherCode)
        {
            var vouchers = await _context.Vouchers.FirstOrDefaultAsync(v => v.RefCourt == courtId && v.VoucherCode == voucherCode);

            return vouchers;
        }


        public async Task<IEnumerable<Voucher?>> GetAllVoucherOfACourtByOwnerIdAsync(string ownerId)
        {
            var vouchers = _context.Vouchers.Include(c=>c.RefCourtNavigation).ThenInclude(c => c.OwnerId == ownerId);

            return vouchers;
        }

    }
}

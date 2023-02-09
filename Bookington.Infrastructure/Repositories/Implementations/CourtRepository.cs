using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class CourtRepository : GenericRepository<Court, BookingtonDbContext>, ICourtRepository
    {
        public CourtRepository(BookingtonDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Court>> QueryAsync(CourtItemQuery query, bool trackChanges = false)
        {
            IQueryable<Court> courts = _context.Courts;
            if (!trackChanges)
            {
                courts = courts.AsNoTracking();
            }
            if (query.OpenAt !=null)
            {
                courts = courts.Where(c => c.OpenAt == query.OpenAt);
            }
            if(query.CloseAt != null)
            {
                courts = courts.Where(c => c.CloseAt == query.CloseAt);
            }
            if(query.OpenAt !=null || query.CloseAt != null)
            {
                courts = courts.Where(c => c.OpenAt == query.OpenAt || c.CloseAt ==query.CloseAt);
            }
            if (!query.SearchText.IsNullOrEmpty())
            {
                courts = courts.Where(c => c.Name == query.SearchText);
            }
            if (!query.District.IsNullOrEmpty())
            {
                var districId = await ReturnDistricId(query.District);

                courts = courts.Where(c => c.DistrictId == districId);
            }
            return courts;

        }
        public async Task<string> ReturnDistricId(string districName)
        {
            
            var result = await _context.Districts.FirstOrDefaultAsync(p => p.DistrictName == districName);
            return result.Id.ToString();
            
        }
    }
}

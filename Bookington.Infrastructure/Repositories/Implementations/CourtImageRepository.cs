using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class CourtImageRepository : GenericRepository<CourtImage, BookingtonDbContext>, ICourtImageRepository
    {
        public CourtImageRepository(BookingtonDbContext context) : base(context)
        {
        }

        public async Task<List<CourtImage>> GetImagesOfCourtByIdAsync(string courtId)
        {
            return await _context.CourtImages.Where(im => im.CourtId == courtId).ToListAsync();
        }
    }
}

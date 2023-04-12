using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ICourtImageRepository :
        IGetAllAsync<CourtImage>,
        IAddAsync<CourtImage>,
        IUpdate<CourtImage>,
        IFindAsync<CourtImage>,
        IDelete<CourtImage>
    {
        Task<List<CourtImage>> GetImagesOfCourtByIdAsync(string courtId);
    }
}

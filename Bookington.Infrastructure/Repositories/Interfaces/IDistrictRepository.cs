using Bookington.Core.Data;
using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IDistrictRepository :
        IGetAllAsync<District>,
        IFindAsync<District>,
        IAddAsync<District>,
        IUpdate<District>
    {
        Task<IEnumerable<District>> GetDistrictsByProviceId(string Id);
    }
}

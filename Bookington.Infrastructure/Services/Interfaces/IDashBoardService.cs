using Bookington.Infrastructure.DTOs.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IDashBoardService
    {
        Task<AdminDashBoardDTO> GetAdminDashBoard(DashBoardQuery query);

        Task<OwnerDashBoardDTO> GetOwnerDashBoard(string ownerId, DashBoardQuery query);
    }
}

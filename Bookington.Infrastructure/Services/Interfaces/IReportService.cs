using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.DTOs.ReportResponse;
using Bookington.Infrastructure.DTOs.SubCourt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IReportService
    {
        // COURT REPORTS RELATED FUNCTIONS
        Task<IEnumerable<CourtReportReadDTO>> GetAllCourtReportsAsync();
        Task<CourtReportReadDTO> CreateCourtReportAsync(CourtReportWriteDTO dto);
        Task<CourtReportReadDTO> UpdateCourtReportAsync(string id, CourtReportWriteDTO dto);
        Task DeleteCourtReportAsync(string id);
        Task<CourtReportReadDTO> GetCourtReportByIdAsync(string id);
        Task<string> HandleCourtReportAsync(CourtReportResponseWriteDTO dto);

        // USER REPORTS RELATED FUNCTIONS
        Task<IEnumerable<UserReportReadDTO>> GetAllUserReportsAsync();
        Task<UserReportReadDTO> CreateUserReportAsync(UserReportCreateDTO dto);
        Task<UserReportReadDTO> UpdateUserReportAsync(string id, UserReportUpdateDTO dto);
        Task DeleteUserReportAsync(string id);
        Task<UserReportReadDTO> GetUserReportByIdAsync(string id);
        Task<string> HandleUserReportAsync(UserReportResponseWriteDTO dto);
    }
}

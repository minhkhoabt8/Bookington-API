using Bookington.Infrastructure.DTOs.Report;
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
        Task<IEnumerable<CourtReportReadDTO>> GetAllCourtReportsAsync();
        Task<CourtReportReadDTO> CreateCourtReportAsync(CourtReportWriteDTO dto);
        Task<CourtReportReadDTO> UpdateCourtReportAsync(string id, CourtReportWriteDTO dto);
        Task DeleteCourtReportAsync(string id);
        Task<CourtReportReadDTO> GetCourtReportByIdAsync(string id);
    }
}

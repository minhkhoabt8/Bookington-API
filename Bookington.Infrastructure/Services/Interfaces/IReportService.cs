using Bookington.Infrastructure.DTOs.Comment;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.DTOs.ReportType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IReportService
    {
        //-------------------------------------------------
        //                     Report
        //-------------------------------------------------
        Task<IEnumerable<ReportReadDTO>> GetAllAsync();
        Task<ReportReadDTO> CreateAsync(ReportWriteDTO dto);
        Task<ReportReadDTO> UpdateAsync(string id, ReportWriteDTO dto);
        Task DeleteAsync(string id);
        Task<ReportReadDTO> GetByIdAsync(string id);
        //-------------------------------------------------
        //                   Report Type
        //-------------------------------------------------
        Task<IEnumerable<ReportTypeReadDTO>> GetAllTypesAsync();
        Task<ReportTypeReadDTO> CreateTypeAsync(ReportTypeWriteDTO dto);
        Task<ReportTypeReadDTO> UpdateTypeAsync(int id, ReportTypeWriteDTO dto);
        Task DeleteTypeAsync(int id);
        Task<ReportTypeReadDTO> GetTypeByIdAsync(int id);
    }
}

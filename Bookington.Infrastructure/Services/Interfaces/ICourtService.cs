
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ICourtService
    {
        Task<PaginatedResponse<CourtReadDTO>> GetAllCourtsByOwnerIdAsync(CourtOfOwnerQuery query);
        Task<CourtReadDTO> CreateAsync(CourtWriteDTO dto);
        Task<CourtReadDTO> UpdateAsync(string id, CourtWriteDTO dto);
        Task DeleteAsync(string id);
        Task<CourtReadDTO> GetByIdAsync(string id);
        Task<PaginatedResponse<CourtQueryResponse>> QueryCourtsAsync(CourtItemQuery query);
    }
}

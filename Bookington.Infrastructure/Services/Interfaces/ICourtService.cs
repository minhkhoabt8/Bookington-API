
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ICourtService
    {
        Task<IEnumerable<CourtReadDTO>> GetAllAsync();
        Task<CourtReadDTO> CreateAsync(CourtWriteDTO dto);
        Task<CourtReadDTO> UpdateAsync(string id, CourtWriteDTO dto);
        Task DeleteAsync(string id);
        Task<CourtReadDTO> GetByIdAsync(string id);
        Task<PaginatedResponse<CourtReadDTO>> QueryCourtsAsync(CourtItemQuery query);
    }
}


using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Microsoft.AspNetCore.Http;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ICourtService
    {
        Task<PaginatedResponse<CourtReadDTO>> GetAllCourtsByOwnerIdAsync(CourtOfOwnerQuery query);
        Task<CourtReadDTO> CreateAsync(CourtWriteDTO dto, IEnumerable<IFormFile> courtImages);
        Task<CourtReadDTO> UpdateAsync(string id, CourtWriteDTO dto, IEnumerable<IFormFile> courtImages);
        Task DeleteAsync(string id);
        Task<CourtReadDTO> GetByIdAsync(string id);
        Task<PaginatedResponse<CourtQueryResponse>> QueryCourtsAsync(CourtItemQuery query);

    }
}

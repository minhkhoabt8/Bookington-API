
using Bookington.Infrastructure.DTOs.Court;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ICourtService
    {
        Task<IEnumerable<CourtReadDTO>> GetAllAsync();
        Task<CourtReadDTO> CreateAsync(CourtWriteDTO dto);
        Task<CourtReadDTO> UpdateAsync(int id, CourtWriteDTO dto);
        Task DeleteAsync(int id);
        Task<CourtReadDTO> GetByIdAsync(int id);
    }
}

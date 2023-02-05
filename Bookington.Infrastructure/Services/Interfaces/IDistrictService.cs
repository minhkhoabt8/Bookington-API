using Bookington.Infrastructure.DTOs.District;


namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IDistrictService
    {
        Task<IEnumerable<DistrictReadDTO>> GetAllAsync();
        Task<DistrictReadDTO> CreateAsync(DistrictWriteDTO dto);
        Task<DistrictReadDTO> UpdateAsync(string id, DistrictWriteDTO dto);

        Task SyncDistrict();
    }
}

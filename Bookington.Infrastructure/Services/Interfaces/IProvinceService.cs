using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Province;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IProvinceService
    {
        Task<IEnumerable<ProvinceReadDTO>> GetAllAsync();
        Task<ProvinceReadDTO> CreateAsync(ProvinceWriteDTO dto);
        Task<ProvinceReadDTO> UpdateAsync(string id, ProvinceWriteDTO dto);

        Task SyncProvince();

    }
}

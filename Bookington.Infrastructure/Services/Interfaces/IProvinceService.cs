using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Province;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IProvinceService
    {
        Task<IEnumerable<ProvinceReadDTO>> GetAllAsync();
        Task<ProvinceReadDTO> Create(ProvinceWriteDTO dto);
        Task<ProvinceReadDTO> Update(string id, ProvinceWriteDTO dto);

    }
}

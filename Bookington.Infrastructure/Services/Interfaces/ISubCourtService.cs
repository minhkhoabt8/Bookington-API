using Bookington.Infrastructure.DTOs.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Infrastructure.DTOs.SubCourt;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ISubCourtService
    {
        Task<IEnumerable<SubCourtReadDTO>> GetAllAsync();
        Task<SubCourtReadDTO> CreateAsync(SubCourtWriteDTO dto);
        Task<SubCourtReadDTO> UpdateAsync(int id, SubCourtWriteDTO dto);
        Task DeleteAsync(int id);
        Task<SubCourtReadDTO> GetByIdAsync(string id);        
    }
}

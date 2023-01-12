using Bookington.Infrastructure.DTOs.Role;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleReadDTO>> GetAllAsync();
        Task<RoleReadDTO> CreateAsync(RoleWriteDTO dto);
        Task<RoleReadDTO> UpdateAsync(int id, RoleWriteDTO dto);
        Task DeleteAsync(int id);
        Task<RoleReadDTO> GetByIdAsync(string id);
        //Task AssignRolesToAccount(Guid accountID, int roleIDs);

    }
}

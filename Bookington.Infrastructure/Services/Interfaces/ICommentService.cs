using Bookington.Infrastructure.DTOs.Comment;
using Bookington.Infrastructure.DTOs.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentReadDTO>> GetAllAsync();
        Task<CommentReadDTO> CreateAsync(CommentWriteDTO dto);
        Task<CommentReadDTO> UpdateAsync(string id, CommentWriteDTO dto);
        Task DeleteAsync(string id);
        Task<CommentReadDTO> GetByIdAsync(string id);
    }
}

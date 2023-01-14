using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Comment
{
    public class CommentWriteDTO
    {
        public string Id { get; set; } = null!;

        // will be disposed later after authorization implementation
        public string CommentWriterId { get; set; } = null!;

        public string RefCourt { get; set; } = null!;

        public string? Content { get; set; } 

        public double? Rating { get; set; }                 
    }
}

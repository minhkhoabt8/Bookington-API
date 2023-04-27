using Bookington.Infrastructure.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Comment
{
    public class CommentReadDTO
    {
        public string Id { get; set; }

        public string CommentWriterId { get; set; } 

        public AccountProfileReadDTO CommentWriter { get; set; }

        public string RefCourt { get; set; }

        public string? Content { get; set; }

        public double? Rating { get; set; }

        public DateTime? CreateAt { get; set; }

        public bool? IsActive { get; set; }
    }

}

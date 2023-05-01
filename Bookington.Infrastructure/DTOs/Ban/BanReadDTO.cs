using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Ban
{
    public class BanReadDTO
    {
        public string Id { get; set; }

        public string? RefAccount { get; set; }

        public string? RefCourt { get; set; }

        public string Reason { get; set; } = null!;

        public int Duration { get; set; }

        public DateTime? BanUntil { get; set; }

        public DateTime CreateAt { get; set; } 

        public bool IsAccountBan { get; set; }

        public bool IsCourtBan { get; set; }

        public bool IsActive { get; set; }

        public Bookington.Core.Entities.Account? RefAccountNavigation { get; set; }

        public Core.Entities.Court? RefCourtNavigation { get; set; }
    }
}

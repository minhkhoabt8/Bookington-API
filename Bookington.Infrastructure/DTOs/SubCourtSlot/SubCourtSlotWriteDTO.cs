using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.SubCourtSlot
{
    public class DefaultSubCourtSlotWriteDTO
    {
        public string SubCourtId { get; set; } = null!;
        public int Price { get; set; }
    }
}

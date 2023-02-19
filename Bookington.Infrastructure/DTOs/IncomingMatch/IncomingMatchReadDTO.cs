using Bookington.Infrastructure.DTOs.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.IncomingMatch
{
    public class IncomingMatchReadDTO
    {
        //ten san
        //ten sub court
        //slot
        //thoi gian dien ra
        // status : incoming, started, finished

        public string PlayDate { get; set; }

        public string CourtName { get; set; }

        public string SubCourtName { get; set; }

        public SlotReadDTO Slot { get; set; }

        public string Status { get; set; } = "*Not Yet Implemented*";

    }
}

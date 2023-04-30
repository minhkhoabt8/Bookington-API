using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.DashBoard
{
    public class DashBoardQuery
    {
        public DateOnly StartTime { get; set; }

        public DateOnly EndTime { get; set; }

    }
}

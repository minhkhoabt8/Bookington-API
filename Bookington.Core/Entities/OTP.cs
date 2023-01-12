using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Core.Entities
{
    public class OTP
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Otp { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual Account? Account { get; set; }
    }
}

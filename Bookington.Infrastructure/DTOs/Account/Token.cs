using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Account
{
    public class Token
    {
        public string Access_token { get; set; }
        public string Id { get; set; }
        public int Expires_in { get; set; }
    }
}

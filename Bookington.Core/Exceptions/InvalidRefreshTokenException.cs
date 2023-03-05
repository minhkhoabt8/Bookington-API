using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Core.Exceptions
{
    public class InvalidRefreshTokenException : HandledException
    {
        public InvalidRefreshTokenException() : base(403, "Missing or invalid refresh token")
        {
        }
    }
}

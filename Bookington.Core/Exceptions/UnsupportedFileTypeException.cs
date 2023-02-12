using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Core.Exceptions
{
    public class UnsupportedFileTypeException :HandledException
    {
        public UnsupportedFileTypeException() : base(400, "The file type is currently not suppported")
        {
        }
    }
}

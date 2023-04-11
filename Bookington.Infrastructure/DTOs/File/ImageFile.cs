using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.File
{
    public class ImageFile
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}

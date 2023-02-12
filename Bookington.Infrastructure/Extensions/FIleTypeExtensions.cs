using Bookington.Core.Exceptions;
using Bookington.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Extensions
{
    public static class FileTypeExtensions
    {
        public static FileType ToFileTypeEnsureSupported(this string type)
        {
            return type switch
            {
                "jpeg" => FileType.JPEG,
                "png" => FileType.PNG,
                
                _ => throw new UnsupportedFileTypeException()
            };
        }
    }
}

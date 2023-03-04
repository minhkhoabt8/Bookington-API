using Bookington.Core.Exceptions;
using Bookington.Infrastructure.Enums;

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

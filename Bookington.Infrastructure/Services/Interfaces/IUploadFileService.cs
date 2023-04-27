using Bookington.Infrastructure.DTOs.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IUploadFileService
    {
        Task<IEnumerable<ImageFile>> GetImageFilesAsync(List<string> fileNames, bool isAccount);
        Task<string> UploadFileAsyncReturnFileName(IFormFile file, bool isAccount = true);

        Task<ImageFile> GetImageFileAsync(string fileName, bool isAccount);
        Task DeleteFileAsync(string fileName, bool isAccount = true);
    }
}

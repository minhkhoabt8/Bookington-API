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
        Task<string> UploadFileAsyncReturnFileName(IFormFile file, bool isAccount = true);

        Task DeleteFileAsync(string fileName, bool isAccount = true);
    }
}

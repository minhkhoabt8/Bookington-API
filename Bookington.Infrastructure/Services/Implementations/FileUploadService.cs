using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;


namespace Bookington.Infrastructure.Services.Implementations
{
    public class FileUploadService : IUploadFileService
    {
        public async Task<string> UploadFileAsyncReturnFileName(IFormFile file, bool IsAccount)
        {
            string path = "";
            if (file.Length > 0)
            {

                if(!IsAccount)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Courts"));
                }
                else
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Accounts"));
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                

                //change file name to a guid
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileName;
            }
            return "";
        }


        public async Task DeleteFileAsync(string fileName, bool isAccount)
        {
            string path = "";

            

            if (!isAccount)
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Courts"));
            }
            else
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Accounts"));
            }

             

            string filePath = Path.Combine(path, fileName);

            if (File.Exists(filePath))
            {
                await Task.Run(() =>
                {
                    File.Delete(filePath);
                });
            }

        }

        private bool CheckIfFileNameExists(string fileName, string directoryPath)
        {
            string filePath = Path.Combine(directoryPath, fileName);
            return File.Exists(filePath);
        }

        
    }
}

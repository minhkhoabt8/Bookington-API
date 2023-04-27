using Bookington.Infrastructure.DTOs.File;
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


        public async Task<IEnumerable<ImageFile>> GetImageFilesAsync(List<string> fileNames, bool isAccount)
        {
            var files = new List<ImageFile>();

            foreach (var fileName in fileNames)
            {
                var path = "";
                if (isAccount)
                {
                    path = Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Accounts", fileName);
                }
                else
                {
                    path = Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Courts", fileName);
                }

                // Check if the file exists
                if (File.Exists(path))
                {
                    // Read the file as a byte array
                    var fileBytes = await File.ReadAllBytesAsync(path);

                    // Create a File object with the file name and byte array
                    var file = new ImageFile
                    {
                        Name = fileName,
                        Content = fileBytes
                    };

                    // Add the File object to the list of files
                    files.Add(file);
                }
            }

            return files;

        }


        public async Task<ImageFile> GetImageFileAsync(string fileName, bool isAccount)
        {
            
                var path = "";
                if (isAccount)
                {
                    path = Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Accounts", fileName);
                }
                else
                {
                    path = Path.Combine(Environment.CurrentDirectory, "..", "Bookington.Infrastructure", "Storages/Courts", fileName);
                }

                // Check if the file exists
                if (File.Exists(path))
                {
                    // Read the file as a byte array
                    var fileBytes = await File.ReadAllBytesAsync(path);

                    // Create a File object with the file name and byte array
                    var file = new ImageFile
                    {
                        Name = fileName,
                        Content = fileBytes
                    };
                    return file;
                }
          
            return null;

        }

    }
}

using ETicaretAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public IWebHostEnvironment webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyToAsync(string fullPath, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task<string> FileRenameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string,string)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            List<(string, string)> datas = new List<(string, string)>();
            foreach (IFormFile file in files)
            {
                string newFileName =await FileRenameAsync(file.FileName);
                string fullPath = Path.Combine(uploadPath, Path.GetExtension(file.FileName));
                bool result= await CopyToAsync(fullPath, file);
                if (!result)
                    return null;
                datas.Add((newFileName, fullPath));
            }
            return datas;
            //todo eğer ki
        }

        
    }
}

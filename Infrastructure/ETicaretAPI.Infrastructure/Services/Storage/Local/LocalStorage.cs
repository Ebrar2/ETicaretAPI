using ETicaretAPI.Application.Abstractions.Storage.Local;
using ETicaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        public IWebHostEnvironment webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public Task DeleteAsync(string fileName, string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFileNames(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string fileName, string pathOrContainerName)
        {
            throw new NotImplementedException();
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
            catch (Exception e)
            {
                throw e;
            }
        }

        string FileRenameAsync(string fileName, string path)
        {
            string fileExtension = Path.GetExtension(fileName);
            string oldFileName = Path.GetFileNameWithoutExtension(fileName);

            string editFileName = NameOperation.CharacterRegulatory(oldFileName);
            string fullName = $"{editFileName}{fileExtension}";
            bool isExist = File.Exists($"{path}\\{fullName}");


            int sayac = 1;
            while (isExist)
            {
                string newName = editFileName + "-" + sayac;
                fullName = $"{newName}{fileExtension}";
                isExist = File.Exists($"{path}\\{fullName}");
                sayac++;
            }
            return fullName;

        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, pathOrContainerName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            List<(string, string)> datas = new List<(string, string)>();
            foreach (IFormFile file in files)
            {
                string newFileName = FileRenameAsync(file.FileName, uploadPath);
                string fullPath = Path.Combine(uploadPath, newFileName);
                bool result = await CopyToAsync(fullPath, file);
                if (!result)
                    return null;
                datas.Add((newFileName, pathOrContainerName));

                
            }

            return datas;
        }
    }
    
}

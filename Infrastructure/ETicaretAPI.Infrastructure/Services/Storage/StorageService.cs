using ETicaretAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private IStorage storage;

        public StorageService(IStorage storage)
        {
            this.storage = storage;
        }

        public string StorageName =>storage.GetType().Name;

        public async Task DeleteAsync(string fileName, string pathOrContainerName)
            => await storage.DeleteAsync(fileName, pathOrContainerName);
        public List<string> GetFileNames(string pathOrContainerName)
        => storage.GetFileNames(pathOrContainerName);

        public bool HasFile(string fileName, string pathOrContainerName)
        => storage.HasFile(fileName, pathOrContainerName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        => storage.UploadAsync(pathOrContainerName,files);
    }
}

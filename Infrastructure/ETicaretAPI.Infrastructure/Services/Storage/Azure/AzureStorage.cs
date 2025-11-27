using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ETicaretAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage :Storage, IAzureStorage
    {
        readonly BlobServiceClient serviceClient;
        BlobContainerClient containerClient;
        public AzureStorage(IConfiguration configuration)
        {
            this.serviceClient = new(configuration["Storages:Azure"]);
         
        }

        public async Task DeleteAsync(string fileName, string pathOrContainerName)
        {
            containerClient = serviceClient.GetBlobContainerClient(pathOrContainerName);
            BlobClient client = containerClient.GetBlobClient(fileName);
            await client.DeleteAsync();
        }

        public List<string> GetFileNames(string pathOrContainerName)
        {
            containerClient = serviceClient.GetBlobContainerClient(pathOrContainerName);
            return containerClient.GetBlobs().Select(c => c.Name).ToList();
        }

        public bool HasFile(string fileName, string pathOrContainerName)
        {
            containerClient = serviceClient.GetBlobContainerClient(pathOrContainerName);
            return containerClient.GetBlobs().Any(b => b.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            containerClient = serviceClient.GetBlobContainerClient(pathOrContainerName);
            await containerClient.CreateIfNotExistsAsync();
            await containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            List<(string fileName, string pathOrContainerName)> datas = new List<(string fileName, string pathOrContainerName)>();
            foreach(IFormFile file in files)
            {
                string newFileName = FileRenameAsync(file.Name, pathOrContainerName, HasFile);
                BlobClient blobClient = containerClient.GetBlobClient(newFileName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((file.FileName, $"{pathOrContainerName}\\{file.FileName}"));
            }
            return datas;
        }
    }
}

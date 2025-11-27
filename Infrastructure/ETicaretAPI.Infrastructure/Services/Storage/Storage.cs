using ETicaretAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string fileName, string pathOrContainerName);
        protected string FileRenameAsync(string fileName, string pathOrContainerName, HasFile hasFile)
        {
            string fileExtension = Path.GetExtension(fileName);
            string oldFileName = Path.GetFileNameWithoutExtension(fileName);

            string editFileName = NameOperation.CharacterRegulatory(oldFileName);
            string fullName = $"{editFileName}{fileExtension}";
            // bool isExist = File.Exists($"{path}\\{fullName}");
            bool isExist = hasFile(fileName, pathOrContainerName);


            int sayac = 1;
            while (isExist)
            {
                string newName = editFileName + "-" + sayac;
                fullName = $"{newName}{fileExtension}";
            //    isExist = File.Exists($"{pathOrContainerName}\\{fullName}");
                isExist = hasFile(fullName, pathOrContainerName);
                sayac++;
            }
            return fullName;

        }
    }
}

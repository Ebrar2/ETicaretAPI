using ETicaretAPI.Application.Abstractions.Services;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
   public  class QRCodeService:IQRCodeService
    {
      

        public byte[] GenerateQRCode(string text)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode pngByteQRCode = new PngByteQRCode(qrCodeData);
            byte[] byteGraphic = pngByteQRCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });
            return byteGraphic;
        }
    }
}

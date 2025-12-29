using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to,string subject,string body,bool isBodyHTML=true);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHTML = true);
       Task SendResetPasswordMailAsync(string to, string username, string userId,string resetToken);
        Task SendOrderCompletedMailAsync(string to, string username, string orderCode);
    }
}

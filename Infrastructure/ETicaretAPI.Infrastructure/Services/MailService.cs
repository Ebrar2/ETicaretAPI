using ETicaretAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHTML = true)
        {
            
            await SendMailAsync(new[] {to}, subject, body, isBodyHTML);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHTML = true)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = isBodyHTML;
            foreach(var to in tos)
              mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.From = new("temelebrar1@gmail.com", "E-Tiaret", System.Text.Encoding.UTF8);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(configuration["Mail:Username"], configuration["Mail:Password"]);
            smtpClient.Port = Int32.Parse(configuration["Mail:Port"]);
            smtpClient.EnableSsl = true;
            smtpClient.Host = configuration["Mail:Host"];
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendOrderCompletedMailAsync(string to, string username, string orderCode)
        {

            StringBuilder mail = new();
            mail.AppendLine("Merhaba " + username + ",<br>" + orderCode + "  No'lu siparişiniz kargoya verilmiştir.");
            mail.AppendLine("<br>İyi günlerde kullanın :)");
            mail.AppendLine("<br><br><br>Saygılarımızla,<br>E-Ticaret");
            await SendMailAsync(new[] { to }, "Siparişiniz Kargoya Verildi", mail.ToString());
        }

        public async Task SendResetPasswordMailAsync(string to,string username,string userId,string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba "+username+",<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiz." +
                "<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(configuration["AngularClientUrl"]+ "password-update/");
            mail.AppendLine(userId+"/"+resetToken+"\"");
            mail.AppendLine(">Yeni şifre talebi için tıkayınız...</a></strong><br><br><span style=\"font-size:12px;\">Not: Eğer bu" +
                "talep tarafınızca gerçekleştirlmediyse lütfen bu maili ciddiye almayınız</span><br><br><br>Saygılarımızla,<br>E-Ticaret");
             await SendMailAsync(new[] {to },"Şifre Yenileme",mail.ToString());

          
        }
    }
}

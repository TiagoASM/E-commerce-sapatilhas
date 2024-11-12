using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.Extensions.Options;


namespace ProjetoEcommerceSapatilhas.Pages.Account
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer = "sandbox.smtp.mailtrap.io"; // Host do Mailtrap
        private readonly int _smtpPort = 2525; // Porta do Mailtrap
        private readonly string _smtpUser = "64b2edb8d18d3d"; // Insira seu usuário
        private readonly string _smtpPass = "d3b4415629507f"; // Insira sua senha

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.EnableSsl = true;

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("noreply@seusite.com"), // Endereço de e-mail de origem
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}


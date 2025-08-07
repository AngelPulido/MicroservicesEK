using Microservices.Ek_Query_Application.Contracts.Persistence;
using Microservices.Ek_Query_Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Microservices.Ek.Query.Infrastructure.Persistence
{
    public class AsyncEmailService : IAsyncServiceEmail
    {
        //private readonly IConfiguration _configuration;

        private readonly EmailSettings _settings;

        public AsyncEmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        //public AsyncEmailService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public async Task<bool> SendEmailWithFileAsync(
            string[] to,
            string subject,
            string body,
            string nameFile,
            string base64Content)
        {
            try
            {
                MailMessage mail = new MailMessage();

                if (!_settings.TestNotification)
                {
                    foreach (var P in to)
                    {
                        mail.To.Add(P.ToString());
                    }

                }
                else
                {
                    mail.To.Clear();
                    mail.CC.Clear();
                    mail.To.Add(_settings.TestNotificationEmail);
                }

                byte[] pdfBytes = Convert.FromBase64String(base64Content);
                MemoryStream pdfStream = new MemoryStream(pdfBytes);
                Attachment pdfb64 = new Attachment(pdfStream, nameFile, MediaTypeNames.Application.Pdf);
                pdfb64.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;


                mail.From = new MailAddress(_settings.FromEmail, _settings.DisplayName, Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = body;
                mail.BodyEncoding = Encoding.UTF8;

                mail.IsBodyHtml = true;
                mail.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), _settings.Priority);
                mail.Attachments.Add(pdfb64);
                //
                SmtpClient client = new SmtpClient();
                if (!string.IsNullOrEmpty(_settings.Username))
                {
                    client.Credentials = new System.Net.NetworkCredential(_settings.Username, _settings.Password);
                }
                if (!string.IsNullOrEmpty(_settings.Port))
                {
                    client.Port = Convert.ToInt32(_settings.Port);
                }
                //
                client.Host = _settings.Servidor;
                client.EnableSsl = _settings.EnableSSL;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                return false;

            }
            return true;
        }
    }
}

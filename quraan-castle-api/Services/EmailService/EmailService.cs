using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace quraan_castle_api.Services
{

    #region Models
    public class SMTPConfigModel
    {
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHTML { get; set; }

    }
    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    #endregion

    public class EmailService
    {
        private const string _templateName = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig = SystemSession.configuration.GetSection("SMTPConfig").Get<SMTPConfigModel>();

        public async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_smtpConfig.SenderAddress);
                foreach(var item in userEmailOptions.ToEmails)
                    mail.To.Add(item);
               
                mail.Subject = userEmailOptions.Subject;
                mail.Body = userEmailOptions.Body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(_smtpConfig.host, 587))
                {
                    smtp.Credentials = new NetworkCredential(_smtpConfig.SenderAddress,_smtpConfig.Password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

        }
        public async Task SendEmail(UserEmailOptions userEmailOptions , string path)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_smtpConfig.SenderAddress);
                foreach (var item in userEmailOptions.ToEmails)
                    mail.To.Add(item);

                mail.Subject = userEmailOptions.Subject;
                mail.Body = userEmailOptions.Body;
                mail.IsBodyHtml = true;

                Attachment sMailAttachment = new Attachment(path);
                mail.Attachments.Add(sMailAttachment);

                using (SmtpClient smtp = new SmtpClient(_smtpConfig.host, 587))
                {
                    smtp.Credentials = new NetworkCredential(_smtpConfig.SenderAddress, _smtpConfig.Password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

        }
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(templateName);
            return body;
        }
    }
}

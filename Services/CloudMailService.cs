using Serilog;
using System.Net;
using System.Net.Mail;

namespace EmailService.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailFrom = default!;
        private readonly string _mailPassword = default!;
        private readonly IEmailLogRepository _emailLogRepository;

        public CloudMailService(IConfiguration configuration, IEmailLogRepository emailLogRepository)
        {
            _mailFrom = configuration["mailSettings:mailFromAddress"];
            _mailPassword = configuration["mailSettings:mailPassword"];
            _emailLogRepository = emailLogRepository;
        }

        const string SMTPADDRESS = "smtp.gmail.com";
        const int PORTNUMBER = 587;

        public async Task SendAsync(string emailTo, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {                
                mail.From = new MailAddress(_mailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(SMTPADDRESS, PORTNUMBER))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_mailFrom, _mailPassword);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(mail);
                }
                Log.Information($"Sent email to {emailTo} about {subject}");
                await _emailLogRepository.PostEmailLog(emailTo, subject);

            }
        }

    }
}

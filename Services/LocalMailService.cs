namespace EmailService.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = default!;
        private readonly string _mailFrom = default!;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];
        }

        public async Task SendAsync(string emailTo, string subject, string body)
        {
            // send mail - output to console window
            Console.WriteLine($"Mail from {emailTo} to {_mailTo}, " + $"with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {body}");
        }
    }
}

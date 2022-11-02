namespace EmailService.Services
{
    public interface IMailService
    {
        Task SendAsync(string subject, string message, string emailTo);
    }
}

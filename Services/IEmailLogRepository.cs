using EmailService.Models;

namespace EmailService.Services
{
    public interface IEmailLogRepository
    {
        public Task<bool> PostEmailLog(string Emailto, string Subject);
    }
}

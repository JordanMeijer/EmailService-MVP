using EmailService.Models;

namespace EmailService.Services
{


    public class EmailLogRepository : IEmailLogRepository
    {
        private readonly DIEmailContext _context;

        public EmailLogRepository(DIEmailContext context)
        {
            _context = context;
        }
        public bool Success { get; set; }

        public async Task<bool> PostEmailLog(string Emailto, string Subject)
        {
            var Time = DateTime.Now;
            var finalEmailLog = new EmailLog()
            {
                EmailTo = Emailto,
                Subject = Subject,
                Time = Time
            };

            await _context.AddAsync(finalEmailLog);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}

using System.ComponentModel.DataAnnotations;

namespace EmailService.Models
{
    public class EmailBodyDto
    {
        const int MAXCHAREMAILADDRESS = 254;
        const int MAXCHARSUBJECT = 230;
        const int MAXCHARBODY = 384000;
  
        [MaxLength(MAXCHAREMAILADDRESS)]
        public string EmailTo { get; set; } = default!;
        
        [MaxLength(MAXCHARSUBJECT)]
        public string Subject { get; set; } = default!;
        
        [MaxLength(MAXCHARBODY)]
        public string Body { get; set; } = default!;
    }
}

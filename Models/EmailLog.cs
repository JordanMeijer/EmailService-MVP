using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace EmailService.Models
{
    /// <summary>
    /// For storing logs of email requests
    /// </summary>
    public partial class EmailLog
    {
        [Key]
        public int EmailLogId { get; set; }
        public string? EmailTo { get; set; }
        public string? Subject { get; set; }
        public DateTime Time { get; set; }
    }
}

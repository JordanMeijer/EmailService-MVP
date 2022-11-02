namespace EmailService.Models
{
    public class ValidationDto
    {
        public bool Valid { get; set; }
        public string Property { get; set; } = default!;
    }
}

using System.Text.RegularExpressions;
using EmailService.Models;


namespace EmailService.Services
{
    public class ValidateService
    {
        public static bool ValidateEmailAddress(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|co.nz)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        public static ValidationDto ValidateEmailBody(EmailBodyDto emailBody)
        {

            ValidationDto validationResult = new ValidationDto();

            if (string.IsNullOrEmpty(emailBody.EmailTo))
            {
                validationResult.Valid = false;
                validationResult.Property = $"Email to is empty";
                return validationResult;
            }
            if (string.IsNullOrEmpty(emailBody.Subject))
            {
                validationResult.Valid = false;
                validationResult.Property = "Email Subject is empty";
                return validationResult;
            }
            if (string.IsNullOrEmpty(emailBody.Body))
            {
                validationResult.Valid = false;
                validationResult.Property = "Email body to is empty";
                return validationResult;
            }
            validationResult.Valid = true;
            validationResult.Property = "Validation Successfull";
            return validationResult;
        }
    }
}

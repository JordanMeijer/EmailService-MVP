using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;
using Dapr;
using Dapr.Client;



namespace EmailService.Controllers
{
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly ILogger<MailController> _logger;
        private readonly IMailService _mailService;
        private readonly DaprClient _dapClient;
        private readonly IBetterMailService _betterMailService;

        public MailController(ILogger<MailController> logger, IMailService mailService, DaprClient daprClient, IBetterMailService betterMailService)
        {
            _logger = logger;
            _mailService = mailService;
            _dapClient = daprClient;
            _betterMailService = betterMailService;
        }

        [HttpPost("sendemail")]
        [Topic("pubsub", "emails")]
        public async Task<ActionResult> SendEmail([FromBody] EmailBodyDto emailBody)
        {
            ValidationDto validResult = ValidateService.ValidateEmailBody(emailBody);

            if ( validResult.Valid == false)
            {return BadRequest(validResult.Property);}

            if (ValidateService.ValidateEmailAddress(emailBody.EmailTo) == false)
            {return BadRequest("please provide a valid email");}

            await _mailService.SendAsync(
                $"{emailBody.EmailTo}",
                $"{emailBody.Subject}",
                $"{emailBody.Body}"
                );
            return NoContent();
        }

        [HttpPost("sendbybettermail")]
        [Topic("pubsub", "bettermail")]
        public async Task<ActionResult> SendBetterMail()
        {
            _betterMailService.SendMail();
            return NoContent();
        }


        [HttpPost("testevent")]
        [Topic("pubsub", "testtopic")]
        public async Task<ActionResult> SendEmail()
        { 
             await _mailService.SendAsync(
                $"jordan.m@digitalisland.co.nz",
                $"Subscribed to test event",
                $"Email Service has subscribed to the topic testtopic and responded with this test email."
                );
            return NoContent();
        }

        [HttpGet("test")]
        public ActionResult Test()
        {return Ok("this is a test");}
        


    }
}

    

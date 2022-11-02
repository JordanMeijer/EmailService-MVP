using BetterMail.Client;
using BetterMail.Client.Model;
using System.Net;

namespace EmailService.Services
{
    public class BetterMailService : IBetterMailService
    {
        private readonly string _serviceUriBase = default!;
        private readonly string _apiKey = default!;

        public BetterMailService(IConfiguration configuration)
        {
            _serviceUriBase = configuration["betterMailSettings:EndPoint"];
            _apiKey = configuration["betterMailSettings:ApiKey"];
        }
        public IBetterMailClient GetApiClient()
        {
            return _buildApiClient();
        }

        public IBetterMailClient _buildApiClient()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //var bmDiKey = commsConfig.BM_DI_KEY;
            return BetterMailClientFactory.SimpleCreate(_serviceUriBase, _apiKey);
        }

        public void SendMail()
        {
            using (var client = _buildApiClient())
            {
                var res = client.TriggerDeployment("f4sr73", "jordan.m@digitalisland.co.nz", new
                {
                    customer = "Dear Customer",
                    content = "Content Here"
                });
            }
        }
    }
}

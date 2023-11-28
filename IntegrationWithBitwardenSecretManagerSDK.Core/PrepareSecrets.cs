using Bitwarden.Sdk;
using Microsoft.Extensions.Configuration;

namespace IntegrationWithBitwardenSecretManagerSDK.Core
{
    public class PrepareSecrets : IPrepareSecrets
    {
        private readonly IConfiguration _configuration;

        public PrepareSecrets(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void GetSecrets()
        {
            var configuration = GetConfiguration();
            using (var bitwardenClient = new BitwardenClient())
            {

            }
        }

        private (string accessToken, string organizationId) GetConfiguration()
        {
            var section = _configuration.GetSection("BitwardenSM");

            if (section == null) throw new ArgumentException();

            var accessToken = section.GetSection("AccessToken").Value;
            var organizationId = section.GetSection("OrganizationId").Value;

            return (accessToken, organizationId);
        }
    }
}

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
        public string GetSecretValue(string secretName, string projectName)
        {
            var configuration = GetConfiguration();
            using (var bitwardenClient = new BitwardenClient())
            {
                bitwardenClient.AccessTokenLogin(configuration.accessToken);
                var secretValue = string.Empty;

                if (CheckIfProjectExists(projectName, configuration.organizationId, bitwardenClient)) return $"project {projectName} not exist";

                var secrets = bitwardenClient.Secrets.List(configuration.organizationId);
                if (secrets != null)
                {
                    var secretsList = secrets.Data.ToList();
                    var secretApikeyId = secretsList.FirstOrDefault(x => x.Key == secretName).Id;

                    var response = bitwardenClient.Secrets.Get(secretApikeyId);
                    secretValue = response.Value;

                    var project = bitwardenClient.Projects.Get(response.ProjectId.Value);
                }

                return secretValue;
            }
        }

        private bool CheckIfProjectExists(string projectName, Guid organizationId, BitwardenClient bitwardenClient)
        {
            var projects = bitwardenClient.Projects.List(organizationId);
            var projectList = projects.Data.ToList();

            var foundedProject = projectList.FirstOrDefault(x => x.Name == projectName);

            return foundedProject == null;
        }

        private (string accessToken, Guid organizationId) GetConfiguration()
        {
            var section = _configuration.GetSection("BitwardenSM");

            if (section == null) throw new ArgumentException();

            var accessToken = section.GetSection("AccessToken").Value;
            var organizationId = Guid.Parse(section.GetSection("OrganizationId").Value);

            return (accessToken, organizationId);
        }
    }
}

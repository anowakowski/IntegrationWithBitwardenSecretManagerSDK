namespace IntegrationWithBitwardenSecretManagerSDK.Core
{
    public interface IPrepareSecrets
    {
        string GetSecretValue(string secretName, string projectName);
    }
}
using IntegrationWithBitwardenSecretManagerSDK.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


IConfigurationBuilder builder = new ConfigurationBuilder();
builder
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


var serviceCollection = new ServiceCollection()
        .AddTransient<IPrepareSecrets, PrepareSecrets>()
        .AddSingleton<IConfiguration>(builder.Build())
        .BuildServiceProvider();

var service = serviceCollection.GetService<IPrepareSecrets>();

service.GetSecrets();


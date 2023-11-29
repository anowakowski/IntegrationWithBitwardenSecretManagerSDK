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

Console.WriteLine(string.Concat("Your secret for Key API_KEY is: ", service.GetSecretValue("API_KEY", "test project")));

Console.WriteLine("press any key to close");
Console.ReadKey();


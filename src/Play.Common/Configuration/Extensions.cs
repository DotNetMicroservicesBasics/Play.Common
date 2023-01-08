using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Play.Common.Settings;

namespace Play.Common.Configuration
{
    public static class Extensions
    {
        public static IConfigurationBuilder ConfigureAzureKeyVault(this IConfigurationBuilder builder)
        {

            var configuration = builder.Build();
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            return builder.AddAzureKeyVault(
                                new Uri($"https://{serviceSettings.KeyVaultName}.vault.azure.net/"),
                                new DefaultAzureCredential()
                                );            
        }

        public static IHostBuilder ConfigureAzureKeyVault(this IHostBuilder hostBuilder){
            return hostBuilder.ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                    {
                        configurationBuilder.ConfigureAzureKeyVault();
                    }
                });                
        }
    }
}
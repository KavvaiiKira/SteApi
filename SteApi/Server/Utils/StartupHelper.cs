using ApiSteaKK;
using Microsoft.Extensions.DependencyInjection;
using SteApi.Server.Services;

namespace SteApi.Server.Utils
{
    public static class StartupHelper
    {
        public static IConfigurationBuilder RegisterConfig(this IConfigurationBuilder configuration)
        {
            configuration.AddJsonFile($"{Environment.CurrentDirectory}\\Config\\config.json");
            return configuration;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection serviceProvider, IConfiguration configuration)
        {
            serviceProvider.AddScoped<ISteApiService>(provider => new SteApiService(configuration.GetValue("ApiKey", string.Empty) ?? string.Empty, provider.GetRequiredService<HttpClient>()));
            serviceProvider.AddScoped<ISteamDataService, SteamDataService>();

            return serviceProvider;
        }
    }
}

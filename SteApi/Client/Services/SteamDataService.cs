using Microsoft.AspNetCore.Components;

namespace SteApi.Client.Services
{
    public class SteamDataService : ISteamDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public SteamDataService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = navigationManager.BaseUri;
        }

        public async Task GetUserAchievements()
        {
            var a = await _httpClient.GetStringAsync($"{_baseUri}api/v1/steamdata/achievements");
        }
    }
}

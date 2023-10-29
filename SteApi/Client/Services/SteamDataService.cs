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

        public async Task<string> GetUserIdByLinkAsync(string userLink)
        {
            userLink = userLink.Replace("https://steamcommunity.com/id/", string.Empty);
            return await _httpClient.GetStringAsync($"{_baseUri}api/v1/steamdata/id/{userLink}");
        }

        public async Task<string> GetUserGamesInfoAsync(string userId, bool includeAppInfo = false)
        {
            return await _httpClient.GetStringAsync($"{_baseUri}api/v1/steamdata/games/{userId}/{includeAppInfo}");
        }

        public async Task<string> GetUserGamesNameAndTimeAsync(string userId)
        {
            return await _httpClient.GetStringAsync($"{_baseUri}api/v1/steamdata/games/nameandtime/{userId}");
        }
    }
}

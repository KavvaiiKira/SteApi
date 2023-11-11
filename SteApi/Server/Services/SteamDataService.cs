using ApiSteaKK.Models;
using ApiSteaKK.Services;

namespace SteApi.Server.Services
{
    public class SteamDataService : ISteamDataService
    {
        private readonly ISteApiService _steApiService;

        public SteamDataService(ISteApiService steApiService)
        {
            _steApiService = steApiService;
        }

        public async Task<string> GetUserIdAsync(string userLink)
        {
            return await _steApiService.GetUserIdByLinkAsync(userLink);
        }

        public async Task<string> GetUserGamesByIdAsync(string userId, bool includeAppInfo = false)
        {
            return await _steApiService.GetGamesByUserIdAsync(userId, includeAppInfo);
        }

        public async Task<string> GetUserGamesNameAndTimeAsync(string userId)
        {
            return await _steApiService.GetFormattedGamesByUserIdAsync(new GamesRequest(userId, appName: true, appPlaytime: true, playTimeFormat: PlayTimeFormats.Minutes));
        }
    }
}

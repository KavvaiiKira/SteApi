using ApiSteaKK;

namespace SteApi.Server.Services
{
    public class SteamDataService : ISteamDataService
    {
        private readonly ISteApiService _steApiService;

        public SteamDataService(ISteApiService steApiService)
        {
            _steApiService = steApiService;
        }

        public async Task GetUserAchievements()
        {
            await _steApiService.GetUserAchievements();
        }
    }
}

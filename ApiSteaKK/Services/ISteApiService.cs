using ApiSteaKK.Models;

namespace ApiSteaKK.Services
{
    public interface ISteApiService
    {
        Task<string> GetUserIdByLinkAsync(string userLink);

        Task<string> GetGamesByUserIdAsync(string userId, bool includeAppInfo = false);

        Task<string> GetFormattedGamesByUserIdAsync(GamesRequest gamesRequest);
    }
}

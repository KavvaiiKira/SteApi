namespace SteApi.Server.Services
{
    public interface ISteamDataService
    {
        Task<string> GetUserIdAsync(string userLink);

        Task<string> GetUserGamesByIdAsync(string userId, bool includeAppInfo = false);

        Task<string> GetUserGamesNameAndTimeAsync(string userId);
    }
}

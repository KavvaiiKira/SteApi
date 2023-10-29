namespace SteApi.Client.Services
{
    public interface ISteamDataService
    {
        Task<string> GetUserIdByLinkAsync(string userLink);

        Task<string> GetUserGamesInfoAsync(string userId, bool includeAppInfo = false);

        Task<string> GetUserGamesNameAndTimeAsync(string userId);
    }
}

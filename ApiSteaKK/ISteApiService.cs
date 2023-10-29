namespace ApiSteaKK
{
    public interface ISteApiService
    {
        Task<string> GetUserIdByLinkAsync(string userLink);

        Task<string> GetGamesByUserIdAsync(string userId, bool includeAppInfo = false);

        Task<string> GetGameNamesAndTimeByUserIdAsync(string userId);
    }
}

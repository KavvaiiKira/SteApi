using Newtonsoft.Json.Linq;

namespace ApiSteaKK
{
    public class SteApiService : ISteApiService
    {
        private readonly HttpClient _httpClient;

        public SteApiService(string apiKey, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException("API Key must not be NULL or EMPTY!", nameof(apiKey));
            }

            Data.Constants.ApiKey = apiKey;

            _httpClient = httpClient;
        }

        public async Task<string> GetUserIdByLinkAsync(string userLink)
        {
            if (string.IsNullOrEmpty(userLink))
            {
                throw new ArgumentNullException("User LINK must not be NULL or EMPTY!");
            }

            var userVanityUrl = userLink.Split('/').Last();

            if (string.IsNullOrEmpty(userVanityUrl))
            {
                userVanityUrl = userLink.Remove(userLink.LastIndexOf('/'), 1).Split('/').Last();
            }

            if (string.IsNullOrEmpty(userVanityUrl))
            {
                throw new ArgumentNullException($"User Vanity Url not found! Parameter Error: {userLink}");
            }

            var requestString = $"{ApiConstants.BaseRequestUrl}/ISteamUser/ResolveVanityURL/v0001/?key={Data.Constants.ApiKey}&vanityurl={userVanityUrl}{ApiConstants.JsonFormatParameter}";
            var response = await _httpClient.GetStringAsync(requestString);
            var responseObj = JObject.Parse(response);

            if (!responseObj.ContainsKey(ResponseConstants.ResponseKey))
            {
                throw new ArgumentNullException($"Get User ID failed! Response data not contsins \"{ResponseConstants.ResponseKey}\" object.");
            }

            var steamIdObj = responseObj[ResponseConstants.ResponseKey] as JObject;

            if (steamIdObj == null || !steamIdObj.ContainsKey(ResponseConstants.SteamIdKet))
            {
                throw new ArgumentNullException($"Get User ID failed! Response data not contsins \"{ResponseConstants.SteamIdKet}\" key.");
            }

            return steamIdObj[ResponseConstants.SteamIdKet]!.ToString();
        }

        public async Task<string> GetGamesByUserIdAsync(string userId, bool includeAppInfo = false)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("User ID must not be NULL or EMPTY!");
            }

            var requestString = $"{ApiConstants.BaseRequestUrl}/IPlayerService/GetOwnedGames/v0001/?key={Data.Constants.ApiKey}&steamid={userId}";

            if (includeAppInfo)
            {
                requestString += ApiConstants.IncludeAppinfoParameter;
            }
            
            requestString += ApiConstants.JsonFormatParameter;

            return await _httpClient.GetStringAsync(requestString);
        }

        public async Task<string> GetGameNamesAndTimeByUserIdAsync(string userId)
        {
            return await GetGamesByUserIdAsync(userId, true);
        }
    }
}

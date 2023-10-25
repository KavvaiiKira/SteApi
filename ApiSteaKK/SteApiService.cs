namespace ApiSteaKK
{
    public class SteApiService : ISteApiService
    {
        private readonly HttpClient _httpClient;

        private const string _jsonFormatParameter = "&format=json";

        public SteApiService(string apiKey, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException("API Key must not be NULL or EMPTY!", nameof(apiKey));
            }

            Data.Constants.ApiKey = apiKey;

            _httpClient = httpClient;
        }

        public async Task GetUserAchievements()
        {
            var requestString = $"http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key={Data.Constants.ApiKey}&vanityurl=kavvaiikira{_jsonFormatParameter}";
            var a = await _httpClient.GetStringAsync(requestString);

            requestString = $"http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=440&key={Data.Constants.ApiKey}&steamid={a}{_jsonFormatParameter}";
            var b = await _httpClient.GetStringAsync(requestString);
        }
    }
}

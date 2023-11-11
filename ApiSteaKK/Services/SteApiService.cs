using ApiSteaKK.Factories;
using ApiSteaKK.Models;
using ApiSteaKK.Utils;
using Newtonsoft.Json.Linq;

namespace ApiSteaKK.Services
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
            var response = await GetGamesAsync(userId, includeAppInfo);
            if (string.IsNullOrEmpty(response))
            {
                return string.Empty;
            }

            var responseObj = JObject.Parse(response);
            if (!responseObj.ContainsKey(ResponseConstants.ResponseKey))
            {
                throw new ArgumentNullException($"Get User Games failed! Response data not contsins \"{ResponseConstants.ResponseKey}\" object.");
            }

            return responseObj[ResponseConstants.ResponseKey]!.ToString();
        }

        public async Task<string> GetFormattedGamesByUserIdAsync(GamesRequest gamesRequest)
        {
            var gamesStr = await GetGamesAsync(gamesRequest.UserId, true);
            if (string.IsNullOrEmpty(gamesStr))
            {
                return string.Empty;
            }

            var responseObj = JObject.Parse(gamesStr);
            if (!responseObj.ContainsKey(ResponseConstants.ResponseKey))
            {
                throw new ArgumentNullException($"Get User Games failed! Response data not contsins \"{ResponseConstants.ResponseKey}\" object.");
            }

            var gamesObj = responseObj[ResponseConstants.ResponseKey] as JObject;
            if (gamesObj == null || !gamesObj.ContainsKey(ResponseConstants.GamesKey))
            {
                throw new ArgumentNullException($"Get User Games failed! Response data not contsins \"{ResponseConstants.GamesKey}\" object.");
            }

            var gamesArray = gamesObj[ResponseConstants.GamesKey] as JArray;
            if (gamesArray == null)
            {
                throw new ArgumentNullException($"Get User Games failed! No games array in response.");
            }

            var formattedGamesObj = new JArray();

            foreach (JObject game in gamesArray)
            {
                formattedGamesObj.Add(GetGameFormattedObject(gamesRequest, game));
            }

            return formattedGamesObj.ToString();
        }

        private JObject GetGameFormattedObject(GamesRequest gamesRequest, JObject gameObj)
        {
            var gameObject = new JObject();

            if (gamesRequest.AppId)
            {
                if (!gameObj.ContainsKey(ResponseConstants.AppId))
                {
                    throw new ArgumentNullException($"Game object not contains \"{ResponseConstants.AppId}\" Key!");
                }

                gameObject.Add(ResponseConstants.AppId, gameObj[ResponseConstants.AppId]);
            }

            if (gamesRequest.AppName)
            {
                if (!gameObj.ContainsKey(ResponseConstants.Name))
                {
                    throw new ArgumentNullException($"Game object not contains \"{ResponseConstants.Name}\" Key!");
                }

                gameObject.Add(ResponseConstants.Name, gameObj[ResponseConstants.Name]);
            }

            if (gamesRequest.AppImage)
            {
                if (!gameObj.ContainsKey(ResponseConstants.ImageIconUrl))
                {
                    throw new ArgumentNullException($"Game object not contains \"{ResponseConstants.ImageIconUrl}\" Key!");
                }

                gameObject.Add(ResponseConstants.ImageIconUrl, gameObj[ResponseConstants.ImageIconUrl]);
            }

            if (gamesRequest.AppPlaytime)
            {
                if (!gameObj.ContainsKey(ResponseConstants.PlayTimeForever))
                {
                    throw new ArgumentNullException($"Game object doesn't contains \"{ResponseConstants.PlayTimeForever}\" Key!");
                }

                gameObject.Add(
                    ResponseConstants.PlayTimeForever,
                    TimeConverter.GetTime(gamesRequest.PlayTimeFormat, gameObj[ResponseConstants.PlayTimeForever]!.Value<string>())
                    );
            }

            if (gamesRequest.AppLastPlayed)
            {
                if (!gameObj.ContainsKey(ResponseConstants.TimeLastPlayed))
                {
                    throw new ArgumentNullException($"Game object not contains \"{ResponseConstants.TimeLastPlayed}\" Key!");
                }

                gameObject.Add(ResponseConstants.TimeLastPlayed, gameObj[ResponseConstants.TimeLastPlayed]);
            }

            return gameObject;
        }

        private async Task<string> GetGamesAsync(string userId, bool includeAppInfo)
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
    }
}

namespace ApiSteaKK.Models
{
    public class GamesRequest
    {
        public string UserId { get; set; } = string.Empty;

        public bool AppId { get; set; } = false;

        public bool AppName { get; set; } = false;

        public bool AppImage { get; set; } = false;

        public bool AppPlaytime { get; set; } = false;

        public PlayTimeFormats PlayTimeFormat { get; set; } = PlayTimeFormats.Minutes;

        public bool AppLastPlayed { get; set; } = false;

        public GamesRequest(string userId)
        {
            UserId = userId;
        }

        public GamesRequest(
            string userId,
            bool appId = false,
            bool appName = false,
            bool appImage = false,
            bool appPlaytime = false,
            PlayTimeFormats playTimeFormat = PlayTimeFormats.Minutes,
            bool appLastPlayed = false)
        {
            UserId = userId;
            AppId = appId;
            AppName = appName;
            AppImage = appImage;
            AppPlaytime = appPlaytime;
            PlayTimeFormat = playTimeFormat;
            AppLastPlayed = appLastPlayed;
        }
    }
}

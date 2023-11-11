namespace SteApi.Client.Pages
{
    public partial class MainPage
    {
        private string _userUrlInput = string.Empty;
        private string _userIdGamesInfoInput = string.Empty;
        private bool _includeAppInfo = false;
        private string _userIdGamesNameAndTimeInput = string.Empty;

        private string _userIdOutput = string.Empty;
        private string _userGamesInfoOutput = string.Empty;
        private string _userGamesNameAndTimeOutput = string.Empty;

        private async Task GetUserIdAsync()
        {
            if (string.IsNullOrEmpty(_userUrlInput))
            {
                return;
            }

            _userIdOutput = await _steamDataService.GetUserIdByLinkAsync(_userUrlInput);

            StateHasChanged();
        }

        private async Task GetUserGamesInfoAsync()
        {
            if (string.IsNullOrEmpty(_userIdGamesInfoInput))
            {
                return;
            }

            _userGamesInfoOutput = await _steamDataService.GetUserGamesInfoAsync(_userIdGamesInfoInput, _includeAppInfo);
        }

        private async Task GetUserGamesNameAndTimeAsync()
        {
            if (string.IsNullOrEmpty(_userIdGamesNameAndTimeInput))
            {
                return;
            }

            _userGamesNameAndTimeOutput = await _steamDataService.GetUserGamesNameAndTimeAsync(_userIdGamesNameAndTimeInput);
        }
    }
}

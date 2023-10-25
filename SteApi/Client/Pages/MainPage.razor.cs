namespace SteApi.Client.Pages
{
    public partial class MainPage
    {
        protected override async Task OnInitializedAsync()
        {
            await _steamDataService.GetUserAchievements();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SteApi.Server.Services;

namespace SteApi.Server.Controllers
{
    [Route("api/v1/steamdata")]
    public class SteamDataController : ControllerBase
    {
        private readonly ISteamDataService _steamDataService;

        public SteamDataController(ISteamDataService steamDataService)
        {
            _steamDataService = steamDataService;
        }

        [HttpGet("achievements")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAchievements()
        {
            await _steamDataService.GetUserAchievements();
            return Ok(string.Empty);
        }
    }
}

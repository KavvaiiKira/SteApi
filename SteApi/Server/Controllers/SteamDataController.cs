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

        [HttpGet("id/{userLink}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserIdAsync(string userLink)
        {
            var res = await _steamDataService.GetUserIdAsync(userLink ?? string.Empty);
            return Ok(res);
        }

        [HttpGet("games/{userId}/{includeAppInfo}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserGamesByIdAsync(string userId, bool includeAppInfo)
        {
            var res = await _steamDataService.GetUserGamesByIdAsync(userId ?? string.Empty, includeAppInfo);
            return Ok(res);
        }

        [HttpGet("games/nameandtime/{userId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserGamesNameAndTimeAsync(string userId)
        {
            var res = await _steamDataService.GetUserGamesNameAndTimeAsync(userId);
            return Ok(res);
        }
    }
}

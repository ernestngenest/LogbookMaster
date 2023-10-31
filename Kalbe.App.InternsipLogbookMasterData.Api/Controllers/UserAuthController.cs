using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Microsoft.AspNetCore.Mvc;
using static Kalbe.App.InternsipLogbookMasterData.Api.Models.UserInternal;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthtService;

        public UserAuthController(IUserAuthService userAuthtService)
        {
            _userAuthtService = userAuthtService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authentication([FromBody] UserLogin userLogin)
        {
            try
            {
                var response = await _userAuthtService.Authenticate(userLogin);
                return response.AccessToken.ToUpper().Contains("ERROR") ? BadRequest(response.AccessToken) : Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}

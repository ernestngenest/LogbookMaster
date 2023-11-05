using Kalbe.App.InternsipLogbookMasterData.Api.Models;
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
        private readonly IUserExternalService _userExternalService;

        public UserAuthController(IUserAuthService userAuthtService, IUserExternalService userExternalService)
        {
            _userAuthtService = userAuthtService;
            _userExternalService = userExternalService;
        }

        [HttpPost("AuthenticateInternal")]
        public async Task<IActionResult> AuthenticationInternal([FromBody] UserLogin userLogin)
        {
            try
            {
                var response = await _userAuthtService.AuthenticateInternal(userLogin);
                return response.AccessToken.ToUpper().Contains("ERROR") ? BadRequest(response.AccessToken) : Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AuthenticateExternal")]
        public async Task<IActionResult> AuthenticationExternal([FromBody] UserExternalLogin userLogin)
        {
            try
            {
                var response = await _userAuthtService.AuthenticateExternal(userLogin);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Equals("Failed Username Not Correct"))
                {
                    return NotFound();
                }
                else if (ex.Message.Equals("Failed Password Not Correct"))
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                else if (ex.Message.Equals("Failed You Not Have Access"))
                {
                    return BadRequest(new { message = "Failed You Not Have Access" });
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserExternal userData)
        {
            try
            {
                var response = await _userExternalService.Save(userData);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

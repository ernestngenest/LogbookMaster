using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : SimpleBaseCrudController<UserRole>
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IDatabaseExceptionHandler _handler;
        public UserRoleController(IUserRoleService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
            _userRoleService = simpleBaseCrud;
            _handler = databaseExceptionHandler;
        }

        [HttpGet("GetAllMentor")]
        public async Task<IActionResult> GetAllMentor()
        {
            try
            {
                var result = await _userRoleService.GetMentor();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

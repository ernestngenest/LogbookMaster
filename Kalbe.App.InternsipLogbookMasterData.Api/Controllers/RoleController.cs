using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RoleController : SimpleBaseCrudController<Role>
    {
        private IRoleService roleService;
        private IDatabaseExceptionHandler _dbHandler;
        public RoleController(IRoleService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
            roleService = simpleBaseCrud;
            _dbHandler = databaseExceptionHandler;
        }

        [HttpGet("GetRoleByUpn")]
        public async Task<IActionResult> GetRoleByUpn(string Upn)
        {
            try
            {
                var result = await roleService.GetRoleByUpn(Upn);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

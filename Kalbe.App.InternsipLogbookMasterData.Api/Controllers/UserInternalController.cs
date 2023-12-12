using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [AllowAnonymous]
    public class UserInternalController : SimpleBaseCrudController<UserInternal>
    {
        private readonly IUserInternalService _userInternalService;
        private readonly IDatabaseExceptionHandler _databaseExceptionHandler;
        public UserInternalController(IUserInternalService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
            _userInternalService = simpleBaseCrud;
            _databaseExceptionHandler = databaseExceptionHandler;
            
        }

        [HttpGet("GetUserInternal")]
        public async Task<IActionResult> GetUserInternal()
        {
            try
            {
                PagedOptions pagedOptions = PagedOptions.GetPagedOptions(base.Request);
                if (pagedOptions == null)
                {
                    return BadRequest("This request require pagination header parameter");
                }

                return Ok(await _userInternalService.GetUserInternal(pagedOptions));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetByUPN/{UPN}")]
        public async Task<IActionResult> GetByUPN(string upn)
        {
            try
            {
                return Ok(await _userInternalService.GetByUPN(upn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserListByRoleCode/{roleCode}")]
        public async Task<IActionResult> GetUserListByRoleCode(string roleCode)
        {
            try
            {
                var result = await _userInternalService.GetUserListByRoleCode(roleCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(long id)
        {
            var existingData = await _userInternalService.GetById(id);
            if (existingData == null)
            {
                return NotFound();
            }

            await _userInternalService.Delete(existingData);
            return Ok();
        }
    }
}

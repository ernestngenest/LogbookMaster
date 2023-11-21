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
    }
}

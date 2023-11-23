
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.App.InternsipLogbookMasterData.API.Objects;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ApprovalController : SimpleBaseCrudController<Approval>
    {
        private readonly IApprovalService _approvalService;
        private readonly IDatabaseExceptionHandler _databaseExceptionHandler;
        public ApprovalController(IApprovalService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
            _approvalService = simpleBaseCrud;
            _databaseExceptionHandler = databaseExceptionHandler;
        }
        protected IActionResult GetResult(IServiceResponse response)
        {
            if (response.IsSuccess())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetData/{SysCode}/{ModuleCode}")]
        public async Task<IActionResult> GetApprovalByModuleCode(string SysCode, string ModuleCode)
        {
            var response = await _approvalService.GetMasterApproval(SysCode, ModuleCode);
            return GetResult(response);
        }
    }
}

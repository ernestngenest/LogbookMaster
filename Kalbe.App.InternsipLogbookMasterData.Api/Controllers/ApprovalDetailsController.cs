using Elastic.CommonSchema;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.App.InternsipLogbookMasterData.API.Objects;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ApprovalDetailsController : ControllerBase
    {
        private readonly IApprovalDetaiilService _approvalDetaiilService;
        public ApprovalDetailsController(IApprovalDetaiilService approvalDetaiilService) 
        {
            _approvalDetaiilService = approvalDetaiilService;
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

        [HttpGet("CurrentWF/Multiple/{AppCode}/{ModuleCode}/{DocNo}")]
        public async Task<IActionResult> GetListCurrentApproval(string AppCode, string ModuleCode, string DocNo)
        {
            string sDocNo = WebUtility.UrlDecode(DocNo);
            var response = await _approvalDetaiilService.GetCurrentApprovalAsync(AppCode, ModuleCode, sDocNo, true);
            return GetResult(response);
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> ApproveData(ApprovalTransactionData _approvalTransactionData)
        {
            var response = await _approvalDetaiilService.ApproveDataAsync(_approvalTransactionData);
            return GetResult(response);
        }

        [HttpPut("DeleteWF")]
        public async Task<IActionResult> DeleteWF(ApprovalTransactionData _obj, [FromQuery] bool notInsertApprovalLog)
        {
            var response = await _approvalDetaiilService.DeleteWFAsync(_obj, notInsertApprovalLog);
            return GetResult(response);
        }

        [HttpPut("Reject")]
        public async Task<IActionResult> DeleteData(ApprovalLogModel _obj)
        {
            var response = await _approvalDetaiilService.Reject(_obj);
            return GetResult(response);
        }

        [HttpPost("Submit")]
        public async Task<IActionResult> PostData(ApprovalTransactionData _approvalTransactionData)
        {
            var response = await _approvalDetaiilService.SubmitDataAsync(_approvalTransactionData);
            return GetResult(response);
        }
    }
}

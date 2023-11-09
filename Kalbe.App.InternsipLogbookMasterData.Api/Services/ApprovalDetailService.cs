using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.API.Objects;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using StackExchange.Redis;
using System.Reflection.Emit;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IApprovalDetaiilService : ISimpleBaseCrud<ApprovalDetail>
    {

    }
    public class ApprovalDetailService : SimpleBaseCrud<ApprovalDetail>, IApprovalDetaiilService
    {
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        public ApprovalDetailService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }

        public async Task <ServiceResponse<int>> SubmitDataAsync(ApprovalTransactionData data)
        {
            var response = new ServiceResponse<int>();

            if(data != null)
            {
                var approvalTransactionList = new List<ApprovalTransactionDataModel>();
                try
                {
                    var message = await CheckWorkFlowByDocNoAsync(data.DocNo);
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    if(string.IsNullOrEmpty(message))
                    {
                        var approvalId = _dbContext.Approvals.AsNoTracking().Where(x => x.ApplicationCode.Equals(data.SystemCode) && x.ModuleCode.Equals(data.ModuleCode) && x.ApprovalLevel.Equals(data.ApprovalLevel)).FirstOrDefault().Id;
                        int i = 0;
                        foreach (var item in data.ApprovalTransactionDataModel)
                        {
                            ApprovalTransactionDataModel approvalTransactionDataModels = new();
                            approvalTransactionDataModels.SystemCode = Constant.SystemCode;
                            approvalTransactionDataModels.ModuleCode = data.ModuleCode;
                            approvalTransactionDataModels.ApprovalLevel = item.ApprovalLevel;
                            approvalTransactionDataModels.DocNo = data.DocNo;
                            approvalTransactionDataModels.EmailPIC = item.EmailPIC;
                            approvalTransactionDataModels.NamePIC = item.NamePIC;
                            approvalTransactionDataModels.PIC = item.PIC;
                            approvalTransactionDataModels.ApprovalLine = item.ApprovalLine;
                            approvalTransactionDataModels.NeedApprove = item.NeedApprove;
                            approvalTransactionDataModels.CreatedByUpn = data.CreatedBy;
                            approvalTransactionDataModels.CreatedByEmail = data.ApproverFromEmail;
                            approvalTransactionDataModels.CreatedByName = data.ApproverFromName;
                            approvalTransactionList.Add(approvalTransactionDataModels);

                            if(item.ApprovalLevel == 1 && i == 0)
                            {
                                data.ApproverTo = item.PIC;
                            }
                            i++;
                        }

                        _dbContext.ApprovalsDetails.AddRange((IEnumerable<ApprovalDetail>)approvalTransactionList);

                        await SendEmail
                    }
                }
            }
        }

        private async Task<string> CheckWorkFlowByDocNoAsync(string docNo)
        {
            string message = "";

            var workFlow = _dbContext.ApprovalsDetails.AsNoTracking().Where(x => x.DocumentNumber.Equals(docNo)).Count();

            if(workFlow > 0)
            {
                message += "<br/>Workflow currently running";
            }
            return message;
        }

        private async Task<string> SendEmail(EmailData _obj, string approverTo, string methodName)
        {
            var serviceResponse = new ServiceResponse<object>();
            try
            {
                // Notification Approval. will pass if model is exist
                if (!string.IsNullOrEmpty(_obj.SystemCode))
                {
                    // please fill emailTo from web for testing. it will skip this line
                    if (string.IsNullOrEmpty(_obj.EmailTo))
                    {
                        _obj.EmailTo = await GetDataByUPN(approverTo, "email");
                    }
                    bool responseEmail = _emailService.EmailNotifictaion(_obj);
                    if (responseEmail)
                    {
                        _logHelper.LogNotification(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Success");
                        serviceResponse.Success = true;
                    }
                    else
                    {
                        _logHelper.LogNotification(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Failed");
                        serviceResponse.Success = false;
                    }

                }
            }
            catch (Exception ex)
            {
                //Insert Log Error
                _logHelper.LogError(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, ex.ToString(), "PostDataWithDueDateAsync");
                _logHelper.LogNotification(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Failed");
                serviceResponse.Success = false;
            }

            return serviceResponse.Success.ToString();
        }
    }
}

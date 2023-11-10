using Elastic.Apm.Api;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Services.ClientService;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.App.InternsipLogbookMasterData.API.Objects;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using StackExchange.Redis;
using System.Reflection.Emit;
using ILogger = Kalbe.Library.Common.Logs.ILogger;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IApprovalDetaiilService : ISimpleBaseCrud<ApprovalDetail>
    {

    }
    public class ApprovalDetailService : SimpleBaseCrud<ApprovalDetail>, IApprovalDetaiilService
    {
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly ILogger _logger;
        private readonly ILoggerHelper _loggerHelper;
        private readonly IUserProfileClientService _userProfileClientService;
        private readonly IEmailServicie _emailService;
        private readonly string messageSuccess = "Success insert workflow approval";
        public ApprovalDetailService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper, IUserProfileClientService userProfileClientService, IEmailServicie emailServicie) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userProfileClientService = userProfileClientService;
            _loggerHelper = loggerHelper;
            _emailService = emailServicie;
        }

        public async Task<ServiceResponse<int>> SubmitDataAsync(ApprovalTransactionData data)
        {
            var response = new ServiceResponse<int>();

            if (data != null)
            {
                var approvalTransactionList = new List<ApprovalTransactionDataModel>();
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var message = await CheckWorkFlowByDocNoAsync(data.DocNo);
                    if (string.IsNullOrEmpty(message))
                    {
                        var approvalId = _dbContext.Approvals.AsNoTracking().Where(x => x.ApplicationCode.Equals(data.SystemCode) && x.ModuleCode.Equals(data.ModuleCode) && x.ApprovalLevel.Equals(data.ApprovalTransactionDataModel.First().ApprovalLevel)).FirstOrDefault().Id;
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

                            if (item.ApprovalLevel == 1 && i == 0)
                            {
                                data.ApproverTo = item.PIC;
                            }
                            i++;
                        }

                        _dbContext.ApprovalsDetails.AddRange((IEnumerable<ApprovalDetail>)approvalTransactionList);

                        await SendEmail(data.EmailData, data.ApproverTo, "Submit");
                        await transaction.CommitAsync();
                        response.Message = messageSuccess;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = message;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    //Insert Log Error
                    //_logHelper.LogError(sysCode, modCode, docNo, ex.ToString(), "PostDataAsync");

                    response.Success = false;
                    response.Message = "Failed insert workflow approval";
                    response.Fail(ex);
                }
            }
            return response;
        }

        private async Task<string> CheckWorkFlowByDocNoAsync(string docNo)
        {
            string message = "";

            var workFlow = _dbContext.ApprovalsDetails.AsNoTracking().Where(x => x.DocumentNumber.Equals(docNo)).Count();

            if (workFlow > 0)
            {
                message += "<br/>Workflow currently running";
            }
            return message;
        }

        private async Task<string> SendEmail(Email _obj, string approverTo, string methodName)
        {
            var serviceResponse = new ServiceResponse<object>();
            try
            {
                // Notification Approval. will pass if model is exist
                // please fill emailTo from web for testing. it will skip this line
                if (string.IsNullOrEmpty(_obj.EmailTo))
                {
                    var userProfile = await _userProfileClientService.GetUserByUPNAsync(approverTo);
                    _obj.EmailTo = userProfile.Email;
                }
                bool responseEmail = _emailService.EmailNotification(_obj);
                var logger = new Models.Commons.Logger();
                if (responseEmail)
                {
                    //_loggerHelper.Save(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Success");
                    serviceResponse.Success = true;
                }
                else
                {
                    //_logHelper.LogNotification(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Failed");
                    serviceResponse.Success = false;
                }
            }
            catch (Exception ex)
            {
                //Insert Log Error
                //_logHelper.LogError(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, ex.ToString(), "PostDataWithDueDateAsync");
                //_logHelper.LogNotification(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Failed");
                serviceResponse.Success = false;
            }

            return serviceResponse.Success.ToString();
        }
    }
}

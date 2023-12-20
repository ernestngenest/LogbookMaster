using Elastic.Apm.Api;
using Elastic.CommonSchema;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Services.ClientService;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.App.InternsipLogbookMasterData.API.Objects;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.Logs;
using Kalbe.Library.Common.Models;
using Kalbe.Library.Data.EntityFrameworkCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using StackExchange.Redis;
using System.Data;
using System.Diagnostics;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Constant = Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons.Constant;
using ILogger = Kalbe.Library.Common.Logs.ILogger;
using Logger = Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons.Logger;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IApprovalDetaiilService : IChildService<ApprovalDetail>
    {
        Task<ServiceResponse<List<ApprovalTransactionDataModel>>> GetCurrentApprovalAsync(string AppCode, string ModuleCode, string DocNo, bool GetMultiple);
        Task<ServiceResponse<int>> SubmitDataAsync(ApprovalTransactionData data);
        Task<ServiceResponse<int>> ApproveDataAsync(ApprovalTransactionData _obj);
        Task<ServiceResponse<int>> DeleteWFAsync(ApprovalTransactionData _obj, bool notInsertApprovalLog);
        Task<ServiceResponse<int>> Reject(ApprovalLogModel _obj);
    }
    public class ApprovalDetailService : ChildService<ApprovalDetail>, IApprovalDetaiilService
    {
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly ILogger _logger;
        private readonly ILoggerHelper _loggerHelper;
        private readonly IUserProfileClientService _userProfileClientService;
        private readonly IEmailService _emailService;
        private readonly string _moduleCode = "Approval Transaction";
        private readonly string messageSuccess = "Success insert workflow approval";
        public ApprovalDetailService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper, IUserProfileClientService userProfileClientService, IEmailService emailServicie) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userProfileClientService = userProfileClientService;
            _loggerHelper = loggerHelper;
            _emailService = emailServicie;
        }


        public async Task<ServiceResponse<List<ApprovalTransactionDataModel>>> GetCurrentApprovalAsync(string AppCode, string ModuleCode, string DocNo, bool GetMultiple)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Save";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion

            var serviceResponse = new ServiceResponse<List<ApprovalTransactionDataModel>>();
            try
            {
                timerFunction.Start();
                timer.Start();

                logData.ExternalEntity += "Start get current approval ";
                logData.PayLoadType += "EF";

                List<ApprovalTransactionDataModel> data = new();
                if(GetMultiple)
                {
                    var dataApproval = _dbContext.ApprovalsDetails.AsNoTracking().Include(s => s.Approval).Where(s => s.Approval.ApplicationCode.Equals(AppCode) && s.Approval.ModuleCode.Equals(ModuleCode) && s.ApproveDate == null && s.DocumentNumber.Equals(DocNo) && !s.IsDeleted).OrderBy(s => s.ApprovalLine).ToList();
                    dataApproval.ForEach(s =>

                    data.Add(new ApprovalTransactionDataModel
                    {
                        SystemCode = Constant.SystemCode,
                        ModuleCode = s.Approval.ModuleCode,
                        ApprovalLevel = s.Approval.ApprovalLevel,
                        ID = s.Id,
                        ApprovalID = s.ApprovalId,
                        DocNo = s.DocumentNumber,
                        EmailPIC = s.EmailPIC,
                        NamePIC = s.NamePIC,
                        PIC = s.PIC,
                        Role = s.Approval.Role,
                        ApprovalLine = s.ApprovalLine,
                        Notes = s.Notes,
                        NeedApprove = s.NeedApprove,
                        ApproveDate = s.ApproveDate,
                        Status = s.Approval.Status
                    }));
                }
                else                                               
                {
                    var dataApproval = _dbContext.ApprovalsDetails.AsNoTracking().Include(s => s.Approval).Where(s => s.Approval.ApplicationCode.Equals(AppCode) && s.Approval.ModuleCode.Equals(ModuleCode) && s.ApproveDate == null && s.DocumentNumber.Equals(DocNo) && !s.IsDeleted).OrderBy(s => s.ApprovalLine).FirstOrDefault();
                        data.Add(new ApprovalTransactionDataModel
                        {
                            SystemCode = Constant.SystemCode,
                            ModuleCode = dataApproval.Approval.ModuleCode,
                            ApprovalLevel = dataApproval.Approval.ApprovalLevel,
                            ID = dataApproval.Id,
                            ApprovalID = dataApproval.ApprovalId,
                            DocNo = dataApproval.DocumentNumber,
                            EmailPIC = dataApproval.EmailPIC,
                            NamePIC = dataApproval.NamePIC,
                            PIC = dataApproval.PIC,
                            Role = dataApproval.Approval.Role,
                            ApprovalLine = dataApproval.ApprovalLine,
                            Notes = dataApproval.Notes,
                            NeedApprove = dataApproval.NeedApprove,
                            ApproveDate = dataApproval.ApproveDate,
                            Status = dataApproval.Approval.Status
                        });
                }
                
                serviceResponse.Data = data;

                timer.Stop();
                logData.ExternalEntity += "End get master approval duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
            }
            catch (Exception ex)
            {
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + ex + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                serviceResponse.Fail(ex);
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> SubmitDataAsync(ApprovalTransactionData data)
        {
            var response = new ServiceResponse<int>();

            if (data != null)
            {
                var approvalTransactionList = new List<ApprovalDetail>();
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
                            ApprovalDetail approvalTransactionDataModels = new();
                            approvalTransactionDataModels.DocumentNumber = data.DocNo;
                            approvalTransactionDataModels.EmailPIC = item.EmailPIC;
                            approvalTransactionDataModels.NamePIC = item.NamePIC;
                            approvalTransactionDataModels.PIC = item.PIC;
                            approvalTransactionDataModels.ApprovalLine = item.ApprovalLine;
                            approvalTransactionDataModels.NeedApprove = item.NeedApprove;
                            approvalTransactionDataModels.CreatedByUpn = data.ApproverFromEmail;
                            approvalTransactionDataModels.CreatedByEmail = data.ApproverFromEmail;
                            approvalTransactionDataModels.CreatedByName = data.ApproverFromName;
                            approvalTransactionDataModels.CreatedDate = DateTime.Now;
                            approvalTransactionDataModels.ApprovalId = approvalId;
                            approvalTransactionList.Add(approvalTransactionDataModels);

                            if (item.ApprovalLevel == 1 && i == 0)
                            {
                                data.ApproverTo = item.PIC;
                            }
                            i++;
                        }

                        _dbContext.ApprovalsDetails.AddRange(approvalTransactionList);

                        //await SendEmail(data.EmailData, data.ApproverTo, "Submit");
                        _dbContext.SaveChanges();
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

            var workFlow = _dbContext.ApprovalsDetails.AsNoTracking().Where(x => x.DocumentNumber.Equals(docNo) && !x.IsDeleted).Count();

            if (workFlow > 0)
            {
                message += "<br/>Workflow currently running";
            }
            return message;
        }

        //private async Task<string> SendEmail(Models.Email _obj, string approverTo, string methodName)
        //{
        //    var serviceResponse = new ServiceResponse<object>();
        //    try
        //    {
        //        // Notification Approval. will pass if model is exist
        //        // please fill emailTo from web for testing. it will skip this line
        //        if (string.IsNullOrEmpty(_obj.EmailTo))
        //        {
        //            var userProfile = await _userProfileClientService.GetUserByUPNAsync(approverTo);
        //            _obj.EmailTo = userProfile.Email;
        //        }
        //        bool responseEmail = _emailService.EmailNotification(_obj);
        //        var logger = new Models.Commons.Logger();
        //        if (responseEmail)
        //        {
        //            //_loggerHelper.Save(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Success");
        //            serviceResponse.Success = true;
        //        }
        //        else
        //        {
        //            //_logHelper.LogNotification(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Failed");
        //            serviceResponse.Success = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Insert Log Error
        //        //_logHelper.LogError(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, ex.ToString(), "PostDataWithDueDateAsync");
        //        //_logHelper.LogNotification(_obj.SystemCode, _obj.ModuleCode, _obj.DocumentNumber, _obj.EmailTo, _obj.EmailCC, _obj.EmailBCC, _obj.EmailSubject, _obj.EmailBody, "Send Email " + methodName + " Failed");
        //        serviceResponse.Success = false;
        //    }

        //    return serviceResponse.Success.ToString();
        //}

        public async Task<ServiceResponse<int>> ApproveDataAsync(ApprovalTransactionData _obj)
        {
            var serviceResponse = new ServiceResponse<int>();

            if (_obj != null)
            {
                string sysCode = _obj.SystemCode;
                string modCode = _obj.ModuleCode;
                string docNo = _obj.DocNo;
                string notes = _obj.Notes;
                string createdBy = _obj.CreatedBy;
                string role = _obj.Role;
                string createdbyname = _obj.ApproverFromName;
                string createdbyemail = _obj.ApproverFromEmail;
                string approverTo = "";
                string sStatus;
                if (string.IsNullOrEmpty(_obj.Status))
                {
                    sStatus = "Approve";
                }
                else
                {
                    sStatus = _obj.Status;
                }

                List<ApprovalTransactionDataModel> approvalTransactionList = new List<ApprovalTransactionDataModel>();

                try
                {

                    //Insert Data
                    foreach (var item in _obj.ApprovalTransactionDataModel)
                    {
                        var _approvalTransactionDataModel = new ApprovalTransactionDataModel();
                        _approvalTransactionDataModel.SystemCode = sysCode;
                        _approvalTransactionDataModel.ModuleCode = modCode;
                        _approvalTransactionDataModel.ApprovalLevel = item.ApprovalLevel;
                        _approvalTransactionDataModel.ID = item.ID;
                        _approvalTransactionDataModel.ApprovalID = item.ApprovalID;
                        _approvalTransactionDataModel.DocNo = docNo;
                        _approvalTransactionDataModel.PIC = item.PIC;
                        _approvalTransactionDataModel.ApprovalLine = item.ApprovalLine;
                        _approvalTransactionDataModel.Notes = item.Notes;
                        _approvalTransactionDataModel.NeedApprove = item.NeedApprove;
                        _approvalTransactionDataModel.ApproveDate = item.ApproveDate;
                        _approvalTransactionDataModel.Status = item.Status;
                        _approvalTransactionDataModel.UpdatedByUpn = createdBy;
                        _approvalTransactionDataModel.UpdatedByEmail = createdbyemail;
                        _approvalTransactionDataModel.UpdatedByName = createdbyname;
                        approvalTransactionList.Add(_approvalTransactionDataModel);
                    }
                    await _dbContext.Database.BeginTransactionAsync();
                    foreach (var item in approvalTransactionList)
                    {
                        var data = _dbContext.ApprovalsDetails.Where(s => s.Id == item.ID).FirstOrDefault();
                        data.ApproveDate = DateTime.Now;
                        _dbContext.Entry(data).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                    await _dbContext.Database.CommitTransactionAsync();

                    //Insert Log Approval
                    //if (string.IsNullOrEmpty(approverTo))
                    //{
                    //    var currWf = await GetCurrentApprovalAsync(_obj.SystemCode, modCode, _obj.DocNo, false);
                    //    int idx = 0;
                    //    foreach (var item in currWf.Data)
                    //    {
                    //        if (idx == 0)
                    //            approverTo = item.PIC;
                    //        else
                    //            approverTo = ";" + item.PIC;
                    //        idx++;
                    //    }
                    //} // Added by TMV - July 11, 2022 - bugfix approverTo empty

                    var approvalEvent = new Logger
                    {
                        ModuleCode = "Approval",
                        DocumentNumber = _obj.DocNo,
                        Activity = "Approve",
                        PayLoad = JsonConvert.SerializeObject(_obj, Formatting.None,
                                            new JsonSerializerSettings()
                                            {
                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                            }),
                    };
                    await _loggerHelper.Save(approvalEvent);
                    //await SendEmail(_obj.EmailData, approverTo, "Approve");

                    serviceResponse.Message = "Success Approve";
                }
                catch (Exception ex)
                {
                    await _dbContext.Database.RollbackTransactionAsync();

                    //Insert Log Error

                    serviceResponse.Success = false;
                    serviceResponse.Message = "Failed Approve";
                    serviceResponse.Fail(ex);
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> DeleteWFAsync(ApprovalTransactionData _obj, bool notInsertApprovalLog)
        {
            var serviceResponse = new ServiceResponse<int>();
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            if (_obj != null)
            {
                string sysCode = _obj.SystemCode;
                string modCode = _obj.ModuleCode;
                string docNo = _obj.DocNo;
                string notes = _obj.Notes;
                string createdBy = _obj.CreatedBy;
                string role = _obj.Role;
                string createdbyname = _obj.ApproverFromName;
                string createdbyemail = _obj.ApproverFromEmail;
                string approverTo = "";

                List<ApprovalTransactionDataModel> approvalTransactionList = new List<ApprovalTransactionDataModel>();

                try
                {

                    //Insert Data
                    int i = 0;
                    foreach (var item in _obj.ApprovalTransactionDataModel)
                    {
                        var _approvalTransactionDataModel = new ApprovalTransactionDataModel();
                        _approvalTransactionDataModel.SystemCode = sysCode;
                        _approvalTransactionDataModel.ModuleCode = modCode;
                        _approvalTransactionDataModel.ApprovalLevel = item.ApprovalLevel;
                        _approvalTransactionDataModel.ID = item.ID;
                        _approvalTransactionDataModel.ApprovalID = item.ApprovalID;
                        _approvalTransactionDataModel.DocNo = docNo;
                        _approvalTransactionDataModel.Role = item.Role;
                        _approvalTransactionDataModel.PIC = item.PIC;
                        _approvalTransactionDataModel.ApprovalLine = item.ApprovalLine;
                        _approvalTransactionDataModel.Notes = item.Notes;
                        _approvalTransactionDataModel.NeedApprove = item.NeedApprove;
                        _approvalTransactionDataModel.ApproveDate = null;
                        _approvalTransactionDataModel.Status = item.Status;
                        _approvalTransactionDataModel.EmailPIC = item.EmailPIC;
                        _approvalTransactionDataModel.NamePIC = item.NamePIC;
                        approvalTransactionList.Add(_approvalTransactionDataModel);
                        approverTo = item.PIC;

                        var toBeDelete = _dbContext.ApprovalsDetails.Where(s => s.DocumentNumber.Equals(item.DocNo) && s.ApprovalId.Equals(item.ApprovalID) && s.PIC.Equals(item.PIC)).FirstOrDefault();
                        _dbContext.ApprovalsDetails.Remove(toBeDelete);
                        //Insert Log Approval
                        if (!notInsertApprovalLog)
                        {
                            var approvalEvent = new Logger
                            {
                                ModuleCode = "Approval",
                                DocumentNumber = docNo,
                                Activity = "Delete WF",
                                PayLoad = JsonConvert.SerializeObject(_obj, Formatting.None,
                                            new JsonSerializerSettings()
                                            {
                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                            }),
                            };
                            await _loggerHelper.Save(approvalEvent);
                        }

                        i++;
                    }
                    await transaction.CommitAsync();
                    //Insert Log Query

                    serviceResponse.Message = "Success delete workflow approval";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    serviceResponse.Success = false;
                    serviceResponse.Message = "Failed delete workflow approval";
                    serviceResponse.Fail(ex);
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> Reject(ApprovalLogModel _obj)
        {
            var serviceResponse = new ServiceResponse<int>();
            if (_obj != null)
            {
                try
                {
                    // Update Table
                    await _dbContext.Database.BeginTransactionAsync();
                    var dataToDelete = _dbContext.ApprovalsDetails.AsNoTracking().Where(s => s.DocumentNumber.Equals(_obj.DocNo)).ToList();
                    _dbContext.ApprovalsDetails.RemoveRange(dataToDelete);
                    _dbContext.SaveChanges();
                    await _dbContext.Database.CommitTransactionAsync();

                    if (_dbContext.ApprovalsDetails.AsNoTracking().Where(s => s.DocumentNumber.Equals(_obj.DocNo)).ToList().Count == 0)
                    {
                        serviceResponse.Data = 1;
                    }
                    else
                    {
                        serviceResponse.Data = 0;
                    }

                    //Insert Log Approval
                    var approvalEvent = new Logger
                    {
                        ModuleCode = "Approval",
                        DocumentNumber = _obj.DocNo,
                        Activity = "Reject",
                        PayLoad = JsonConvert.SerializeObject(_obj, Formatting.None,
                                            new JsonSerializerSettings()
                                            {
                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                            }),
                    };
                    await _loggerHelper.Save(approvalEvent);

                    //// Notification Approval. will pass if model is exist
                    //if (!string.IsNullOrEmpty(_obj.EmailData.SystemCode))
                    //{
                    //    // please fill emailTo from web for testing. it will skip this line
                    //    if (string.IsNullOrEmpty(_obj.EmailData.EmailTo))
                    //    {
                    //        _obj.EmailData.EmailTo = await GetDataByUPN(_obj.ApproverTo, "email");
                    //    }
                    //    _logHelper.EmailNotifictaion(_obj.EmailData);
                    //}
                    //await SendEmail(_obj.EmailData, _obj.ApproverTo, "Reject");
                    serviceResponse.Message = messageSuccess;// "Success insert workflow approval";
                }
                catch (Exception ex)
                {
                    //Insert Log Error
                    var approvalEvent = new Logger
                    {
                        ModuleCode = "Approval",
                        DocumentNumber = _obj.DocNo,
                        Activity = "Reject",
                        PayLoad = JsonConvert.SerializeObject(_obj, Formatting.None,
                                            new JsonSerializerSettings()
                                            {
                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                            }),
                        Message = "Error : " + ex.Message
                    };
                    await _loggerHelper.Save(approvalEvent);
                    await _dbContext.Database.RollbackTransactionAsync();
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                    serviceResponse.Fail(ex);
                }
            }
            return serviceResponse;
        }

    }
}

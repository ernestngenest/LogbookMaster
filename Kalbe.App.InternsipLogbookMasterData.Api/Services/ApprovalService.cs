using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.App.InternsipLogbookMasterData.API.Objects;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.Logs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ILogger = Kalbe.Library.Common.Logs.ILogger;
using Logger = Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons.Logger;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IApprovalService : ISimpleBaseCrud<Approval>
    {
        Task<ServiceResponse<IEnumerable<Approval>>> GetMasterApproval(string AppCode, string ModuleCode);
    }
    public class ApprovalService : SimpleBaseCrud<Approval>, IApprovalService
    {
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly string _moduleCode = "Approval";
        private readonly ILogger _logger;
        private readonly ILoggerHelper _loggerHelper;
        public ApprovalService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper) : base(logger, dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _loggerHelper = loggerHelper;
        }

        public async Task<ServiceResponse<IEnumerable<Approval>>> GetMasterApproval(string AppCode, string ModuleCode)
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

            var serviceResponse = new ServiceResponse<IEnumerable<Approval>>();
            try
            {
                timerFunction.Start();
                timer.Start();

                logData.ExternalEntity += "Start get master approval ";
                logData.PayLoadType += "EF";

                var data = await _dbContext.Approvals.AsNoTracking().Where(s => s.ApplicationCode == AppCode && s.ModuleCode == ModuleCode).ToListAsync();

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
    }
}

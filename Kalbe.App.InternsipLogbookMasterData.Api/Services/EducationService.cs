using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IEducationService : ISimpleBaseCrud<Education>
    {

    }
    public class EducationService : SimpleBaseCrud<Education>, IEducationService
    {
        private readonly Library.Common.Logs.ILogger _logger;
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly ILoggerHelper _loggerHelper;
        private readonly IAllowanceService _allowanceService;
        private readonly string _moduleCode = "Education Allowance";
        public EducationService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper, IAllowanceService allowanceService) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _loggerHelper = loggerHelper;
            _allowanceService = allowanceService;
        }

        public override async Task<IEnumerable<Education>> GetAll()
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get All";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion

            try
            {
                timerFunction.Start();
                timer.Start();
                logData.ExternalEntity += "1. Start Get All Data";
                logData.PayLoadType += "Entity Framework";

                var data = await _dbContext.Educations.AsNoTracking()
                        .Include(s => s.Allowances).Where(s => !s.IsDeleted).ToListAsync();

                timer.Stop();
                logData.ExternalEntity += "End get all data duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call Save : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);

                return data;
            }
            catch (Exception ex)
            {
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + ex + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                throw;
            }
        }

        public override async Task<Education> GetById(long id)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get By Id";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion

            try
            {
                timerFunction.Start();
                timer.Start();
                logData.ExternalEntity += "1. Start Get By Id";
                logData.PayLoadType += "Entity Framework";

                var data = _dbContext.Educations.AsNoTracking()
                        .Include(s => s.Allowances).Where(s => !s.IsDeleted && s.Id == id).FirstOrDefault();

                timer.Stop();
                logData.ExternalEntity += "End get by id duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call Save : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);

                return data;
            }
            catch (Exception ex)
            {
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + ex + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                throw;
            }
        }

        public override async Task Update(Education data)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Update";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            using var transation = _dbContext.Database.BeginTransaction();
            try
            {

                timerFunction.Start();

                timer.Start();
                logData.ExternalEntity += "Start Update ";
                logData.PayLoadType += "Entity Framework";
                _dbContext.Entry(data).State = EntityState.Modified;
                var existingAllowance = await _dbContext.Allowances.AsNoTracking().Where(x => x.EducationId  == data.Id).ToListAsync();

                await _allowanceService.Update(existingAllowance, data.Allowances);
                await _dbContext.SaveChangesAsync();
                await transation.CommitAsync();
                timer.Stop();
                logData.ExternalEntity += "End Update duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
            }
            catch (Exception x)
            {
                await transation.RollbackAsync();
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + x + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                throw;
            }
        }
    }
}

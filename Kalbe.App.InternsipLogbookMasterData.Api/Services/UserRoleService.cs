using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserRoleService : ISimpleBaseCrud<UserRole>
    {
        Task<IEnumerable<UserRole>> GetMentor();
    }
    public class UserRoleService : SimpleBaseCrud<UserRole>, IUserRoleService
    {
        private readonly string _moduleCode = "User Role";
        private readonly Library.Common.Logs.ILogger _logger;
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly ILoggerHelper _loggerHelper;
        public UserRoleService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _loggerHelper = loggerHelper;
        }

        public async Task<IEnumerable<UserRole>> GetMentor()
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get Unconfirmed User";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();

                timer.Start();
                logData.ExternalEntity += "1. Start Get Mentor Data";
                logData.LogType += "EF";



                var data = _dbContext.UserRoles.AsNoTracking()
                            .Where(s => s.RoleCode.Equals("MENTOR")).ToList();

                timer.Stop();
                logData.ExternalEntity += "End get mentor duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
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
    }
}

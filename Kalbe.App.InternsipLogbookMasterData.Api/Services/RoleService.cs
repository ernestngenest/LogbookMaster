using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ILogger = Kalbe.Library.Common.Logs.ILogger;
using Logger = Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons.Logger;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IRoleService : ISimpleBaseCrud<Role>
    {
        Task<IEnumerable<Role>> GetRoleByUpn(string Upn);
    }
    public class RoleService : SimpleBaseCrud<Role>, IRoleService
    {
        private readonly ILogger _logger;
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly string _moduleCode = "Role";
        private readonly ILoggerHelper _loggerHelper;
        public RoleService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _loggerHelper = loggerHelper;
        }


        public override async Task<IEnumerable<Role>> GetAll()
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
                logData.ExternalEntity += "Start Get All ";
                logData.PayLoadType += "Entity Framework";

                var data = _dbContext.Roles.AsNoTracking()
                    .OrderBy(s => s.Id).ToList();

                timer.Stop();
                logData.ExternalEntity += "End Get All duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                return data;
            }
            catch (Exception x)
            {
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + x + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetRoleByUpn(string Upn)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get Role By Upn";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                timer.Start();
                logData.ExternalEntity += "Start Get User Role By Upn ";
                logData.PayLoadType += "Entity Framework";

                var data = _dbContext.UserRoles.AsNoTracking().Where(s => s.UserPrincipalName.Equals(Upn) && !s.IsDeleted)
                    .OrderBy(s => s.Id).ToList();

                var roles = new List<Role>();
                foreach (var item in data)
                {
                    var role = _dbContext.Roles.AsNoTracking().Where(s => s.RoleCode.Equals(item.RoleCode)).FirstOrDefault();
                    roles.Add(role);
                }
                timer.Stop();
                logData.ExternalEntity += "End Get User Role by Upn duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                return roles;
            }
            catch (Exception x)
            {
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + x + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                throw;
            }
        }
    }
}

using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.EntityFramework.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog.Events;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserInternalService : ISimpleBaseCrud<UserInternal>
    {
        Task<PagedList<UserInternal>> GetUserInternal(PagedOptions pagedOptions);
    }
    public class UserInternalService : SimpleBaseCrud<UserInternal>, IUserInternalService
    {
        private readonly byte[] salt = new byte[128 / 8];
        private readonly int iterationCount = 10000;
        private readonly int byteRequested = 256 / 8;
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly ILoggerHelper _loggerHelper;
        private readonly string _moduleCode = "User Internal";
        private readonly Library.Common.Logs.ILogger _logger;
        public UserInternalService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper) : base(logger, dbContext)
        {
            _dbContext = dbContext;
            _loggerHelper = loggerHelper;
            _logger = logger;
        }

        

        public override async Task<UserInternal> Save(UserInternal data)
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
            try
            {
                timerFunction.Start();
                logData.PayLoad = JsonConvert.SerializeObject(data);

                data.Password = Encrypt(data.Password);
                _logger.Log(LogEventLevel.Debug, " Creating new " + typeof(UserInternal).Name + " data to database");

                timer.Start();
                logData.ExternalEntity += "Start Save ";
                logData.PayLoadType += "Entity Framework";
                data.UserRoles.ForEach(r => { r.UserPrincipalName = data.UserPrincipalName; r.Email = data.Email; r.Name = data.Name; });
                _dbContext.Add(data);
                await _dbContext.SaveChangesAsync();
                timer.Stop();
                logData.ExternalEntity += "End Save duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

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

        public string Encrypt(string password)
        {
            try
            {
                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: iterationCount,
                    numBytesRequested: byteRequested));
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<PagedList<UserInternal>> GetUserInternal(PagedOptions pagedOptions)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get User Internal PagedList";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                if(pagedOptions == null)
                {
                    throw new Exception("Pagedoptions is empty, please check header");
                }
                timerFunction.Start();
                timer.Start();

                timer.Start();
                logData.ExternalEntity += "1. Start Get User Internal Data";
                logData.LogType += "EF";

                var data = _dbContext.UserInternals.AsNoTracking()
                            .Include(s => s.UserRoles.Where(x => !x.IsDeleted))
                            .Where(x => !x.IsDeleted);

                timer.Stop();
                logData.ExternalEntity += "End get mentor duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timer.Start();
                logData.ExternalEntity += "2. Start Get Pagedlist";
                logData.LogType += "EF";



                var result = await PagedList<UserInternal>.GetPagedList(data, pagedOptions);

                timer.Stop();
                logData.ExternalEntity += "End get mentor duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                return result;
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

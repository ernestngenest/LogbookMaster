using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Services.ClientService;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using MassTransit.JobService;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using static Kalbe.App.InternsipLogbookMasterData.Api.Models.UserInternal;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserAuthService
    {
        Task<UserReturn> AuthenticateInternal(UserLogin data);
        Task<AuthResult> AuthenticateExternal(UserExternalLogin DataUser);
    }
    public class UserAuthService : IUserAuthService
    {
        private readonly IAuthClientService _authClientService;
        private readonly IJwtService _jwtService;
        private readonly string _moduleCode = "User Authentication";
        private readonly ILoggerHelper _loggerHelper;
        private readonly IUserExternalService _userExternalService;
        private InternsipLogbookMasterDataDataContext _dbContext;

        public UserAuthService(InternsipLogbookMasterDataDataContext dbContext, IAuthClientService authClientService, IUserExternalService userExternalService, IJwtService jwtService, ILoggerHelper loggerHelper)
        {
            _dbContext = dbContext;
            _authClientService = authClientService;
            _userExternalService = userExternalService;
            _jwtService = jwtService;
            _loggerHelper = loggerHelper;
        }

        public async Task<UserReturn> AuthenticateInternal(UserLogin data)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Authenticate";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                logData.DocumentNumber = data.Username;
                logData.PayLoad = JsonConvert.SerializeObject(data);

                timer.Start();
                logData.ExternalEntity += "Start LoginAsync ";
                logData.PayLoadType += "EF, ";
                var respUser = await _dbContext.UserInternals
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .FirstOrDefaultAsync(x => x.UserPrincipalName == data.Username);
                timer.Stop();
                logData.ExternalEntity += "End LoginAsync duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

                if (respUser == null)
                {
                    return null;
                }
                //if (respUser.Roles.Count == 0)
                //{
                //    throw new Exception("User Dont Have Acces To This Application");
                //}

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);

                return new UserReturn()
                {
                    Upn = respUser.UserPrincipalName,
                    Name = respUser.Name,
                    AccessToken = _jwtService.GenerateTokenInternal(respUser)
                };
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

        public async Task<AuthResult> AuthenticateExternal(UserExternalLogin DataUser)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Authenticate External";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                logData.DocumentNumber = DataUser.Username;
                logData.PayLoad = JsonConvert.SerializeObject(DataUser);
                timer.Start();
                logData.ExternalEntity += "Start Get Data UserExternal ";
                logData.PayLoadType += "Entity Framework";
                //Get data
                var data = await _dbContext.UserExternals
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .FirstOrDefaultAsync(x => x.UserPrincipalName == DataUser.Username);
                timer.Stop();
                logData.ExternalEntity += "End Get Data UserExternal duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

                if (data == null)
                {
                    throw new InvalidOperationException("Failed Username Not Correct");
                }

                //Compare password
                DataUser.Password = _userExternalService.Encrypt(DataUser.Password);
                if (DataUser.Password != data.Password)
                {
                    throw new InvalidOperationException("Failed Password Not Correct");
                }

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);

                return new AuthResult()
                {
                    UserName = DataUser.Username,
                    AccessToken = _jwtService.GenerateToken(data)
                };
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

using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using MassTransit.JobService;
using Newtonsoft.Json;
using System.Diagnostics;
using static Kalbe.App.InternsipLogbookMasterData.Api.Models.UserInternal;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserAuthService
    {
        Task<UserReturn> Authenticate(UserLogin data);
    }
    public class UserAuthService : IUserAuthService
    {
        private readonly IAuthClientService _authClientService;
        private readonly IJwtService _jwtService;
        private readonly string _moduleCode = "UserInternal";
        private readonly ILoggerHelper _loggerHelper;

        public UserAuthService(IAuthClientService authClientService, IJwtService jwtService, ILoggerHelper loggerHelper)
        {
            _authClientService = authClientService;
            _jwtService = jwtService;
            _loggerHelper = loggerHelper;
        }

        public async Task<UserReturn> Authenticate(UserLogin data)
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
                logData.PayLoadType += "API Authenticate";
                var respUser = await _authClientService.LoginAsync(data);
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
    }
}

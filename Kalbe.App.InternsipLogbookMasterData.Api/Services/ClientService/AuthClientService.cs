using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static Kalbe.App.InternsipLogbookMasterData.Api.Models.UserInternal;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services.ClientService
{
    public interface IAuthClientService
    {
        Task<UserInternal> LoginAsync(UserLogin data);
    }
    public class AuthClientService : IAuthClientService
    {
        private readonly HttpClient _client;
        private readonly AppSettingModel _settingModel;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ILoggerHelper _loggerHelper;
        private readonly IConfiguration _configuration;

        public AuthClientService(IConfiguration configuration, HttpClient client, IOptions<AppSettingModel> settings, IOptions<JwtConfiguration> jwtConfiguration, ILoggerHelper loggerHelper)
        {
            _settingModel = settings.Value;
            client.BaseAddress = new Uri(_settingModel.Authentication);
            client.DefaultRequestHeaders.Add("Accept", _settingModel.MediaType);
            _client = client;
            _configuration = configuration;
            _jwtConfiguration = jwtConfiguration.Value;
            _loggerHelper = loggerHelper;
        }

        public async Task<UserInternal> LoginAsync(UserLogin data)
        {
            #region log data
            Logger logData = new()
            {
                CreatedDate = DateTime.Now,
                AppCode = Constant.SystemCode,
                ModuleCode = Constant.ModuleCode,
                LogType = "Information",
                Activity = "LoginAsync ",
                DocumentNumber = data.Username,
                PayLoad = JsonConvert.SerializeObject(data) + ". "
            };

            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion

            try
            {
                timerFunction.Start();
                UserInternal dataUserInternal = new();
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, _settingModel.MediaType);

                timer.Start();
                logData.ExternalEntity += "1. Start Post API Global Login ";
                logData.PayLoadType += "API Global Login";
                var response = await _client.PostAsync("Login", content);
                timer.Stop();
                logData.ExternalEntity += "End Post API Global Login duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                if (response.IsSuccessStatusCode)
                {
                    var sRoles = "";
                    var responseString = await response.Content.ReadAsStringAsync();
                    dynamic jsonObj = JsonConvert.DeserializeObject<ExpandoObject>(responseString, new ExpandoObjectConverter());
                    if (Utils.IsTokenValid(jsonObj.accessToken, _jwtConfiguration.Secret, out JwtSecurityToken securityToken))
                    {
                        dataUserInternal.UserPrincipalName = Utils.GetClaim(securityToken, Constant.UserPrincipalName);
                        dataUserInternal.NIK = Utils.GetClaim(securityToken, Constant.NIK);
                        dataUserInternal.Name = Utils.GetClaim(securityToken, Constant.Name);
                        dataUserInternal.Email = Utils.GetClaim(securityToken, Constant.Email);
                        dataUserInternal.JobTitle = Utils.GetClaim(securityToken, Constant.JobTtlName);
                        dataUserInternal.DeptName = Utils.GetClaim(securityToken, Constant.DeptName);
                        dataUserInternal.CompCode = Utils.GetClaim(securityToken, Constant.CompCode);
                        dataUserInternal.CompName = Utils.GetClaim(securityToken, Constant.CompName);
                        sRoles = Utils.GetClaim(securityToken, Constant.Roles);
                    }
                    dataUserInternal.UserRoles = JsonConvert.DeserializeObject<List<UserRole>>(sRoles);
                }
                else
                {
                    throw new Exception(Constant.LoginFailedMsg);
                }

                timerFunction.Stop();
                logData.Message += "Duration Call Save : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);

                return dataUserInternal;
            }
            catch (Exception ex)
            {
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + ex + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                throw new InvalidOperationException(ex.Message.ToString());
            }
        }
    }
}

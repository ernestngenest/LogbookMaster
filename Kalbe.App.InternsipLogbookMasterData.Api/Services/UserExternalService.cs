using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.Logs;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog.Events;
using System.Diagnostics;
using System.Text;
using static MassTransit.ValidationResultExtensions;
using ILogger = Kalbe.Library.Common.Logs.ILogger;
using Logger = Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons.Logger;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserExternalService : ISimpleBaseCrud<UserExternal>
    {
        Task<UserExternal> Save(UserExternal data);
        Task<UserExternal> Update(UserExternal data);
        Task Delete(UserExternal data);
        string Encrypt(string password);

        Task<IEnumerable<UserExternal>> GetUnconfirmedUser();
        Task<UserExternal> ConfirmUser(long id);
        Task<PagedList<UserExternal>> GetUserExternal(PagedOptions pagedOptions);
        Task<Mentor> GetMentorByUPN(string UPN);
    }
    public class UserExternalService : SimpleBaseCrud<UserExternal>, IUserExternalService
    {
        private readonly byte[] salt = new byte[128 / 8];
        private readonly int iterationCount = 10000;
        private readonly int byteRequested = 256 / 8;
        private readonly string _moduleCode = "User External";
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        private readonly ILoggerHelper _loggerHelper;
        private readonly ILogger _logger;
        private readonly AppSettingModel _settingModel;
        private readonly IOptions<AppSettingModel> _appSettings;
        private readonly IEmailService _emailService;

        public UserExternalService(ILogger logger, InternsipLogbookMasterDataDataContext dbContext, ILoggerHelper loggerHelper, IOptions<AppSettingModel> appSettings, IEmailService emailService) : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _loggerHelper = loggerHelper;
            _appSettings = appSettings;
            _settingModel = _appSettings.Value;
            _emailService = emailService;
        }

        public override async Task<UserExternal> Save(UserExternal data)
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
                data.Status = "Unconfirmed";
                _logger.Log(LogEventLevel.Debug, " Creating new " + typeof(UserExternal).Name + " data to database");

                timer.Start();
                logData.ExternalEntity += "Start Save ";
                logData.PayLoadType += "Entity Framework";
                data.Status = "Unconfirmed";
                data.UserRole.RoleCode = "INTERN";
                data.UserRole.RoleName = "Intern";
                data.UserRole.UserPrincipalName = data.UserPrincipalName;
                data.UserRole.Name = data.Name;
                data.EndDate = data.JoinDate.AddDays(30 * data.InternshipPeriodMonth);
                _dbContext.Set<UserExternal>().Add(data);
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
        public override async Task<UserExternal> Update(UserExternal data)
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
            try
            {

                timerFunction.Start();
                //timer.Start();
                //logData.ExternalEntity += "Start Get Data ";
                //logData.PayLoadType += "Entity Framework";
                //var oldData = _dbContext.UserExternals.AsNoTracking().FirstOrDefault(s => s.Id == data.Id);
                //data.
                //await _dbContext.SaveChangesAsync();
                //timer.Stop();
                //logData.ExternalEntity += "End Update duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

                timer.Start();
                logData.ExternalEntity += "Start Update ";
                logData.PayLoadType += "Entity Framework";
                data.Status = "Confirmed";
                _dbContext.Update(data);
                await _dbContext.SaveChangesAsync();
                timer.Stop();
                logData.ExternalEntity += "End Update duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

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

        public override async Task Delete(UserExternal data)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Delete";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                logData.DocumentNumber = data.Id.ToString();
                logData.PayLoad = JsonConvert.SerializeObject(data);

                timer.Start();
                logData.ExternalEntity += "Start Delete ";
                logData.PayLoadType += "Entity Framework";
                await base.Delete(data);
                timer.Stop();
                logData.ExternalEntity += "End Delete duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
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

        public override async Task<UserExternal> GetById(long id)
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
                logData.ExternalEntity += "Start Get By Id ";
                logData.PayLoadType += "Entity Framework";

                var data = _dbContext.UserExternals.AsNoTracking()
                    .Include(x => x.UserRole)
                    .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();

                timer.Stop();
                logData.ExternalEntity += "End Get By Id duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
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

        // Generate a random password of a given length (optional)
        //public string RandomPassword(int size = 0)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append(RandomString(4, true));
        //    builder.Append(RandomNumber(1000, 9999));
        //    builder.Append(RandomString(2, false));
        //    return builder.ToString();
        //}
        //public int RandomNumber(int min, int max)
        //{
        //    Random random = new Random();
        //    return random.Next(min, max);
        //}
        //public string RandomString(int size, bool lowerCase)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    Random random = new Random();
        //    char ch;
        //    for (int i = 0; i < size; i++)
        //    {
        //        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        //        builder.Append(ch);
        //    }
        //    if (lowerCase)
        //        return builder.ToString().ToLower();
        //    return builder.ToString();
        //}

        public async Task<IEnumerable<UserExternal>> GetUnconfirmedUser()
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
                logData.ExternalEntity += "1. Start Get Unconfirmed Data";
                logData.PayLoadType += "Entity Framework";

                var data = _dbContext.UserExternals.AsNoTracking().Where(x => x.Status.Equals("Unconfirmed")).ToList();

                timer.Stop();
                logData.ExternalEntity += "End get unconfirmed data duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
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

        public async Task<UserExternal> ConfirmUser(long id)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Confirm User";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                timerFunction.Start();
                timer.Start();

                logData.ExternalEntity += "Start get update confirm status ";
                logData.PayLoadType += "EF";

                var dataUser = await GetById(id);

                dataUser.Status = "Confirmed";

                _dbContext.Entry(dataUser).State = EntityState.Modified;

                _dbContext.SaveChanges();
                await transaction.CommitAsync();

                timer.Stop();
                logData.ExternalEntity += "End update confirm status duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timerFunction.Stop();
                logData.Message += "Duration Call Update Confirm : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);

                return dataUser;

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

        public async Task<PagedList<UserExternal>> GetUserExternal(PagedOptions pagedOptions)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get User External PagedList";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                if (pagedOptions == null)
                {
                    throw new Exception("Pagedoptions is empty, please check header");
                }
                timerFunction.Start();
                timer.Start();

                timer.Start();
                logData.ExternalEntity += "1. Start Get User External Data";
                logData.LogType += "EF";

                var data = _dbContext.UserExternals.AsNoTracking()
                            .Include(s => s.UserRole)
                            .Where(x => !x.IsDeleted);

                timer.Stop();
                logData.ExternalEntity += "End get user external duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();

                timer.Start();
                logData.ExternalEntity += "2. Start Get Pagedlist";
                logData.PayLoadType += "EF";



                var result = await PagedList<UserExternal>.GetPagedList(data, pagedOptions);

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

        public async Task<Mentor> GetMentorByUPN(string UPN)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get Mentor By UPN";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                timer.Start();

                timer.Start();
                logData.ExternalEntity += "1. Start Get Mentor Data";
                logData.PayLoadType += "EF";

                var data = _dbContext.UserExternals.AsNoTracking()
                            .Where(x => !x.IsDeleted && x.UserPrincipalName == UPN).FirstOrDefault();

                var mentor = new Mentor
                {
                    MentorName = data.MentorName,
                    MentorUPN = data.MentorUpn
,                   MentorEmail = _dbContext.UserInternals.AsNoTracking().FirstOrDefault(s => s.UserPrincipalName == data.MentorUpn).Email,
                };

                timer.Stop();
                logData.ExternalEntity += "End get Get Mentor Data duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                timer.Reset();


                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                return mentor;
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
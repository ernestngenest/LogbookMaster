using Elasticsearch.Net;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Common.EntityFramework.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using Serilog.Events;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserInternalService : ISimpleBaseCrud<UserInternal>
    {
        Task<PagedList<UserInternal>> GetUserInternal(PagedOptions pagedOptions);
        Task<UserInternal> GetByUPN(string upn);
        Task<IEnumerable<UserInternal>> GetUserListByRoleCode(string roleCode);
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

        public override async Task<UserInternal> Update(UserInternal data)
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
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                timerFunction.Start();
                timer.Start();
                logData.ExternalEntity += "Start Update";
                logData.PayLoadType += "Entity Framework";


                //if role change
                //foreach( var item in data.UserRoles)
                //{
                //    var tempData = _dbContext.UserRoles.AsNoTracking().Where(s => s.RoleCode == item.RoleCode).FirstOrDefault();

                //    if (tempData == null) 
                //    {
                //        _dbContext.UserRoles.Add(item);
                //    }
                //}
                _dbContext.Entry(data).State = EntityState.Modified;
                var newData = data.UserRoles;
                var existingData = await _dbContext.UserRoles.AsNoTracking().Where(s => s.UserInternalId == data.Id && !s.IsDeleted).ToListAsync();
                IEnumerable<UserRole> productsToBeInserted = newData.Where(x => !existingData.Select(y => y.RoleCode).Contains(x.RoleCode));
                int inserted = (productsToBeInserted.Any() ? productsToBeInserted.Count() : 0);
                if (inserted > 0)
                {
                    foreach (var item in productsToBeInserted)
                    {
                        item.UserPrincipalName = data.UserPrincipalName;
                        item.Email = data.Email; item.Name = data.Name;
                        item.UserInternalId = data.Id;
                    };
                }
                IEnumerable<UserRole> productsToBeDeleted = existingData.Where(x => !newData.Any(y => y.RoleCode.Contains(x.RoleCode)));
                int deleted = (productsToBeDeleted.Any() ? productsToBeDeleted.Count() : 0);
                _dbContext.UserRoles.AddRange(productsToBeInserted);
                _dbContext.UserRoles.RemoveRange(productsToBeDeleted);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                timer.Stop();
                logData.ExternalEntity += "End Update duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";

                timerFunction.Stop();
                logData.Message += "Duration Call : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                return data;
            }
            catch (Exception x)
            {
                await transaction.RollbackAsync();
                timerFunction.Stop();
                logData.LogType = "Error";
                logData.Message += "Error " + x + ". Duration : " + timerFunction.Elapsed.ToString(@"m\:ss\.fff") + ". ";
                await _loggerHelper.Save(logData);
                throw;
            }
        }

        public override async Task<UserInternal> GetById(long id)
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

                var data = _dbContext.UserInternals.AsNoTracking()
                    .Include(x => x.UserRoles.Where(s => !s.IsDeleted))
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
                if (pagedOptions == null)
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
                var searchTrim = pagedOptions.SearchString.Trim(new Char[] { ' ', '*' });
                string[] filterArray;
                if (result.TotalData == 0 && !pagedOptions.SearchString.IsNullOrEmpty())
                {
                    var dataRole = _dbContext.UserInternals
                    .AsNoTracking()
                    .Include(s => s.UserRoles.Where(x => !x.IsDeleted))
                    .Where(s => !s.IsDeleted && (s.UserRoles.Any(x => x.RoleCode.Contains(searchTrim)) || s.UserRoles.Any(x => x.RoleName.Contains(searchTrim))));

                    pagedOptions.SearchString = "";
                    result = await PagedList<UserInternal>.GetPagedList(dataRole, pagedOptions);
                }
                //for filter
                else if (!pagedOptions.FilterString.IsNullOrEmpty() && pagedOptions.FilterString.Contains("userRoles"))
                {
                    filterArray = pagedOptions.FilterString.Trim(new Char[] { ' ', '*' }).Split("|");
                    searchTrim = "";
                    var param = "userRoles=";
                    List<string> filter = new();
                    if (filterArray.Length == 1)
                    {
                        //namaYangTerlibat 
                        if (filterArray[0].Contains(param))
                        {
                            searchTrim = filterArray[0].Substring(param.Length, filterArray[0].Length - param.Length);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < filterArray.Length; i++)
                        {
                            if (filterArray[i].StartsWith(param))
                            {
                                searchTrim = filterArray[i].Substring(param.Length, filterArray[i].Length - param.Length);
                            }
                            else
                            {
                                filter.Add(filterArray[i]);
                            }
                        }
                    }
                    if (filter.Count > 1)
                    {
                        pagedOptions.FilterString = String.Join("|", filter);
                    }
                    else
                    {
                        pagedOptions.FilterString = "";
                    }
                    var dataRole = _dbContext.UserInternals
                    .AsNoTracking()
                    .Include(s => s.UserRoles.Where(x => !x.IsDeleted))
                    .Where(s => !s.IsDeleted && (s.UserRoles.Any(x => x.RoleCode.Contains(searchTrim)) || s.UserRoles.Any(x => x.RoleName.Contains(searchTrim))));
                    result = await PagedList<UserInternal>.GetPagedList(dataRole, pagedOptions);
                }

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

        public async Task<UserInternal> GetByUPN(string upn)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get By Upn";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                timer.Start();

                timer.Start();
                logData.ExternalEntity += "1. Start Get User Internal Data";
                logData.LogType += "EF";

                var data = _dbContext.UserInternals.AsNoTracking()
                            .Include(s => s.UserRoles.Where(x => !x.IsDeleted))
                            .Where(x => !x.IsDeleted && x.UserPrincipalName.Equals(upn)).FirstOrDefault();

                timer.Stop();
                logData.ExternalEntity += "End get user interbal duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
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

        public async Task<IEnumerable<UserInternal>> GetUserListByRoleCode(string roleCode)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get User List By Role Code";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                timer.Start();

                timer.Start();
                logData.ExternalEntity += "1. Start Get User List By Role Code";
                logData.LogType += "EF";

                var data = _dbContext.UserInternals.AsNoTracking()
                            .Include(s => s.UserRoles.Where(x => !x.IsDeleted))
                            .Where(x => !x.IsDeleted && x.UserRoles.Any(s => s.RoleCode.Equals(roleCode))).ToList();

                timer.Stop();
                logData.ExternalEntity += "End Get User List By Role Code duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
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

        public async Task<IEnumerable<UserExternal>> GetMenteeByUpn(string upn)
        {
            #region log data
            Logger logData = new Logger();
            logData.CreatedDate = DateTime.Now;
            logData.ModuleCode = _moduleCode;
            logData.LogType = "Information";
            logData.Activity = "Get Mentee By UPN";
            var timer = new Stopwatch();
            var timerFunction = new Stopwatch();
            #endregion
            try
            {
                timerFunction.Start();
                timer.Start();

                timer.Start();
                logData.ExternalEntity += "1. Start Get User External By UPN";
                logData.LogType += "EF";

                var data = _dbContext.UserExternals.AsNoTracking()
                            .Where(s => s.MentorUpn == upn).ToList();

                timer.Stop();
                logData.ExternalEntity += "End Get Get User External By UPN duration : " + timer.Elapsed.ToString(@"m\:ss\.fff") + ". ";
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

using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.Library.Common.EntityFramework.Data;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Utilities
{
    public interface ILoggerHelper : ISimpleBaseCrud<Logger>
    {
    }

    public class LoggerHelper : SimpleBaseCrud<Logger>, ILoggerHelper
    {
        private readonly InternsipLogbookMasterDataDataContext _dbContext;
        public LoggerHelper(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
            _dbContext = dbContext;
        }
        public async override Task<Logger> Save(Logger data)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Loggers.Add(data);
                await _dbContext.SaveChangesAsync();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

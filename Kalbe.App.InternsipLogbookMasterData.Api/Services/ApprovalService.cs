using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IApprovalService
    {

    }
    public class ApprovalService : SimpleBaseCrud<Approval>, IApprovalService
    {
        private InternsipLogbookMasterDataDataContext _dbContext;
        public ApprovalService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Approval> GetMasterApproval(string AppCode, string ModuleCode)
        {

        }
    }
}

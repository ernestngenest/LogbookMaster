using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IAllowanceService : ISimpleBaseCrud<Allowance>
    {

    }
    public class AllowanceService : SimpleBaseCrud<Allowance>, IAllowanceService
    {
        public AllowanceService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IDepartmentService : ISimpleBaseCrud<Department>
    {

    }
    public class DepartmentService : SimpleBaseCrud<Department>, IDepartmentService
    {
        public DepartmentService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

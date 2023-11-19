using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IRoleService : ISimpleBaseCrud<Role>
    {

    }
    public class RoleService : SimpleBaseCrud<Role>, IRoleService
    {
        public RoleService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

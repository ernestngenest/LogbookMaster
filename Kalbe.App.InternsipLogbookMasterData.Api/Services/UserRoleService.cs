using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserRoleService : ISimpleBaseCrud<UserRole>
    {

    }
    public class UserRoleService : SimpleBaseCrud<UserRole>, IUserRoleService
    {
        public UserRoleService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

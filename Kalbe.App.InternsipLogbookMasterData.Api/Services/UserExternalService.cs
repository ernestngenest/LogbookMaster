using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IUserExternalService : ISimpleBaseCrud<UserExternal>
    {

    }
    public class UserExternalService : SimpleBaseCrud<UserExternal>, IUserExternalService
    {
        public UserExternalService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

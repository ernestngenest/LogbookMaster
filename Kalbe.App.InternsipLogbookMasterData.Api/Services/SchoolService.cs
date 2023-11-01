using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface ISchoolService : ISimpleBaseCrud<School>
    {

    }
    public class SchoolService : SimpleBaseCrud<School>, ISchoolService
    {
        public SchoolService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

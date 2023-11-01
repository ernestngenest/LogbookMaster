using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface IFacultyService : ISimpleBaseCrud<Faculty>
    {

    }
    public class FacultyService : SimpleBaseCrud<Faculty>, IFacultyService
    {
        public FacultyService(Library.Common.Logs.ILogger logger, InternsipLogbookMasterDataDataContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

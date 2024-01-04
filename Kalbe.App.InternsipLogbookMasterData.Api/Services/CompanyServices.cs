using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public interface ICompanyServices : ISimpleBaseCrud<Company>
    {

    }
    public class CompanyServices : SimpleBaseCrud<Company>
    {
        public CompanyServices(Library.Common.Logs.ILogger logger, DbContext dbContext) : base(logger, dbContext)
        {
        }
    }
}

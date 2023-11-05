using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    public class AllowanceController : SimpleBaseCrudController<Allowance>
    {
        public AllowanceController(ISimpleBaseCrud<Allowance> simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
        }
    }
}

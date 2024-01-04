using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CompanyController : SimpleBaseCrudController<Company>
    {
        public CompanyController( ICompanyServices simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
        }
    }
}

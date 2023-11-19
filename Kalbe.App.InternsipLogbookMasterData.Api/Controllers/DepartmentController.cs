using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : SimpleBaseCrudController<Department>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IDatabaseExceptionHandler _databaseExceptionHandler;
        public DepartmentController(IDepartmentService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
            _departmentService = simpleBaseCrud;
            _databaseExceptionHandler = databaseExceptionHandler;
        }
    }
}

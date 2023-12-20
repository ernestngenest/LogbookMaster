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
    public class EducationController : SimpleBaseCrudController<Education>
    {
        private readonly IEducationService educationService;
        private readonly IDatabaseExceptionHandler dbHandler;

        public EducationController(IEducationService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
            educationService = simpleBaseCrud;
            dbHandler = databaseExceptionHandler;
        }

        [HttpGet("GetByEducation/{educationCode}")]
        public async Task<IActionResult> GetByEducation(string educationCode)
        {
            try
            {
                var result = await educationService.GetByEducation(educationCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}

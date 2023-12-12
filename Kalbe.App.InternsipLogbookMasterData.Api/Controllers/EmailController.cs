using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly IDatabaseExceptionHandler _databaseExceptionHandler;
        public EmailController(IEmailService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler)
        {
            emailService = simpleBaseCrud;
            _databaseExceptionHandler = databaseExceptionHandler;
        }


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(Email mail)
        {
            try
            {
                await emailService.SendEmailAsync(mail);
                return Ok(mail);
            }
            catch (InvalidOperationException ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

    }
}

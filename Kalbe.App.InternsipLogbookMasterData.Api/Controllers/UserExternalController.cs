using Elastic.Apm.Api;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.EntityFramework.Controllers;
using Kalbe.Library.Common.EntityFramework.Data;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserExternalController : SimpleBaseCrudController<UserExternal>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserExternalService _service;
        private readonly IDatabaseExceptionHandler _handler;
        public UserExternalController(IUserExternalService simpleBaseCrud, IDatabaseExceptionHandler databaseExceptionHandler) : base(simpleBaseCrud, databaseExceptionHandler)
        {
            _service = simpleBaseCrud;
            _handler = databaseExceptionHandler;
        }

        [HttpGet("GetUnconfirmedIntern")]
        public async Task<IActionResult> GetUnconfrimedIntern()
        {
            try
            {
                var result = await _service.GetUnconfirmedUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ConfirmUser/{id}")]
        public async Task<IActionResult> ConfirmUser(long id)
        {
            try
            {
                var result = await _service.ConfirmUser(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("ReviseUser")]
        //public async Task<IActionResult> ReviseUser([FromBody] UserExternal data)
        //{
        //    try
        //    {
        //        var result = await _service.Update(data);
        //        return Ok(result);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet("GetUserExternal")]
        public async Task<IActionResult> GetUserExternal()
        {
            try
            {
                PagedOptions pagedOptions = PagedOptions.GetPagedOptions(base.Request);
                if (pagedOptions == null)
                {
                    return BadRequest("This request require pagination header parameter");
                }

                return Ok(await _service.GetUserExternal(pagedOptions));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMentorByUPN/{UPN}")]
        public async Task<IActionResult> GetMentorByUPN(string UPN)
        {
            try
            {
                var result = await _service.GetMentorByUPN(UPN);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("data not found");
            }
        }
    }
}

using Application.ModelInterfaces.DtoInterfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects;
using WebApi.Constants;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class ExceptionLogsController : ControllerBase
    {
        private readonly ExceptionLogService exceptionLogService;

        public ExceptionLogsController(ExceptionLogService exceptionLogService)
        {
            this.exceptionLogService = exceptionLogService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] ExceptionLogDto dto)
        {
            await exceptionLogService.Create(dto);

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IExceptionLogDto[]> GetByDates([FromQuery] DateTimeOffset start, [FromQuery] DateTimeOffset end)
        {
            return await exceptionLogService.GetByDates(start, end);
        }
    }
}

using System.ComponentModel.DataAnnotations;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class TermsController : GenericCrudController<Term, TermDto>
    {
        public TermsController(GenericCrudService<Term, TermDto> service) : base(service)
        {
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<TermDto>> GetList([Required] int page, [Required] int pageSize) //todo check validity of page and pagesize
        {
            return service.GetAsync(page, pageSize);
        }

    }
}

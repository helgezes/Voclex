using System.ComponentModel.DataAnnotations;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class TermsController : ControllerBase
    {
        private readonly GenericCrudService<Term, TermDto> service;

        public TermsController(GenericCrudService<Term, TermDto> service)
        {
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(TermDto dto)
        {
            if (dto.Id != default(int)) 
                return BadRequest("This is method for creation. Id should be 0. For updating existing object use different method.");

            try
            {
                await service.Create(dto);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Creation failed. Check your model and try again.");
            }

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get([Required]int id)
        {
            var result = await service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<TermDto>> GetList([Required] int page, [Required] int pageSize) //todo check validity of page and pagesize
        {
            return service.GetAsync(page, pageSize);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(TermDto dto)
        {
            return OkOrNotFound(await service.UpdateAsync(dto) != null);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([Required] int id)
        {
            return OkOrNotFound(await service.Delete(id));
        }

        private ActionResult OkOrNotFound(bool condition)
        {
            if (condition)
                return Ok();

            return NotFound();
        }
    }
}

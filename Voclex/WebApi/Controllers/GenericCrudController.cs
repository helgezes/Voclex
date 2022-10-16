using System.ComponentModel.DataAnnotations;
using Application.ModelInterfaces;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebApi.Controllers
{
    public abstract class GenericCrudController<TModel, TDto> : ControllerBase
        where TModel : class, IIdentifiable where TDto : class, IIdentifiable 
    {
        protected readonly GenericCrudService<TModel, TDto> service;

        protected GenericCrudController(GenericCrudService<TModel, TDto> service)
        {
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult> Create(TDto dto)
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
        public virtual async Task<ActionResult> Get([Required] int id)
        {
            var result = await service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult> Update(TDto dto)
        {
            return OkOrNotFound(await service.UpdateAsync(dto) != null);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult> Delete([Required] int id)
        {
            return OkOrNotFound(await service.Delete(id));
        }

        protected ActionResult OkOrNotFound(bool condition)
        {
            if (condition)
                return Ok();

            return NotFound();
        }
    }
}

using System.ComponentModel.DataAnnotations;
using Application.ModelInterfaces;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public abstract class GenericCrudController<TModel, TDto> : ControllerBase
        where TModel : class, IIdentifiable where TDto : class, IIdentifiable 
    {
        protected readonly ICrudService<TModel, TDto> crudService;

        protected GenericCrudController(ICrudService<TModel, TDto> crudService)
        {
            this.crudService = crudService;
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
                var newModel = await crudService.Create(dto);

                return Ok(newModel.Id);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Creation failed. Check your model and try again.");
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult> Read([Required] int id)
        {
            var result = await crudService.GetByIdAsync(id);
            return this.OkWithResultOrNotFound(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult> Update(TDto dto)
        {
            return this.OkOrNotFound(await crudService.UpdateAsync(dto) != null);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult> Delete([Required] int id)
        {
            return this.OkOrNotFound(await crudService.Delete(id));
        }
    }
}

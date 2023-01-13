using Application.ModelInterfaces;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Queries.TermsRelated;

namespace WebApi.Controllers;

[Route("[controller]")]
public abstract class GenericTermsRelatedController<TModel, TDto> :
    GenericCrudController<TModel, TDto>
    where TModel : class, ITermRelated, IIdentifiable
    where TDto : class, IIdentifiable
{
    protected readonly ITermRelatedService<TDto> service;

    protected GenericTermsRelatedController(ITermRelatedService<TDto> service,
	    ICrudService<TModel, TDto> crudService) : base(crudService)
    {
        this.service = service;
    }

    [HttpGet(nameof(GetList))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IEnumerable<TDto>> GetList([FromQuery] TermsRelatedListQuery termsRelatedListQuery)
    {
        return service.GetListAsync(termsRelatedListQuery.TermsIds);
    }
}
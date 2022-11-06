using Application.ModelInterfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Queries.TermsRelated;

namespace WebApi.Controllers;

[Route("[controller]")]
public abstract class GenericTermsRelatedController<TModel, TDto> :
    GenericCrudController<TModel, TDto>
    where TModel : class, ITermRelated, IIdentifiable
    where TDto : class, IIdentifiable
{
    protected readonly TermRelatedService<TModel, TDto> service;

    protected GenericTermsRelatedController(TermRelatedService<TModel, TDto> service,
        GenericCrudService<TModel, TDto> genericCrudService) : base(genericCrudService)
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
using Application.ModelInterfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Queries.TermsRelated;

namespace WebApi.Controllers;

[Route("[controller]")]
public abstract class GenericTermsRelatedController<TModel, TDto> : 
    ControllerBase where TModel : class, ITermRelated
{
    protected readonly TermRelatedService<TModel, TDto> service;

    protected GenericTermsRelatedController(TermRelatedService<TModel, TDto> service)
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
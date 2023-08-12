using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects.Queries.TermsRelated;
using SharedLibrary.ModelInterfaces;
using WebApi.Constants;

namespace WebApi.Controllers;

[Route("[controller]")]
public abstract class GenericTermsRelatedController<TModel, TDto> :
    GenericCrudController<TModel, TDto>
    where TModel : class, ITermRelated, IIdentifiable
    where TDto : class, IIdentifiable
{
    protected readonly ITermRelatedService<TDto> termRelatedService;

    protected GenericTermsRelatedController(ITermRelatedService<TDto> termRelatedService,
	    ICrudService<TModel, TDto> crudService) : base(crudService)
    {
        this.termRelatedService = termRelatedService;
    }

    [HttpGet(nameof(GetList))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = CacheConstants.ModulesGetListCacheDurationInSeconds, Location = ResponseCacheLocation.Client)]
    public Task<IEnumerable<TDto>> GetList([FromQuery] TermsRelatedListQuery termsRelatedListQuery)
    {
        return termRelatedService.GetListAsync(termsRelatedListQuery.TermsIds);
    }
}
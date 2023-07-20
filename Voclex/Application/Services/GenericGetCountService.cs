using Application.DataAccess;
using Application.Services.QueryServices;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class GenericGetCountService<TModel, TQueryDto>
	where TModel : class
{
	private readonly IDbContext context;
	private readonly IQueryService<TModel, TQueryDto> queryService;


	public GenericGetCountService(IDbContext context, IQueryService<TModel, TQueryDto> queryService)
	{
		this.context = context;
		this.queryService = queryService;
	}

	public Task<int> GetCount(TQueryDto dto)
	{
		return QueryContext(dto).CountAsync();
	}

	private IQueryable<TModel> QueryContext(TQueryDto dto)
	{
		return context.Set<TModel>()
			.AsNoTracking()
			.Where(queryService.WhereQuery(context, dto));
	}
}
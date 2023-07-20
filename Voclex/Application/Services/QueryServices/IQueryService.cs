using Application.DataAccess;
using System.Linq.Expressions;

namespace Application.Services.QueryServices
{
	public interface  IQueryService<TModel, TQueryDto>
	{
		Expression<Func<TModel, bool>> WhereQuery(IDbContext context, TQueryDto dto);
	}
}

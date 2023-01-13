using System.Linq.Expressions;
using Application.DataAccess;
using Application.ModelInterfaces;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class GenericGetListService<TModel, TDto> : IGetListService<TModel, TDto> where TModel : class, IIdentifiable where TDto : class, IIdentifiable
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;

        public GenericGetListService(IDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAsync(IListQuery<TModel> query)
        {
            return await QueryContext(query)
                .OrderBy(m => m.Id)
                .Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                .ProjectTo<TDto>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public Task<int> GetCount(IQuery<TModel> query)
        {
            return QueryContext(query).CountAsync();
        }

        private IQueryable<TModel> QueryContext(IQuery<TModel> query)
        {
            return context.Set<TModel>()
                .AsNoTracking()
                .Where(query.WhereQuery(context));
        }
    }

    public interface IListQuery<TModel> : IQuery<TModel>
    {
        int Page { get; }

        int PageSize { get; }
    }

    public interface IQuery<TModel>
    {
        Expression<Func<TModel, bool>> WhereQuery(IDbContext context);
    }
}

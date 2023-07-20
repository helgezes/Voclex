using Application.DataAccess;
using Application.Services.QueryServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataTransferObjects.Queries.Interfaces;
using SharedLibrary.ModelInterfaces;

namespace Application.Services
{
	public class GenericGetListService<TModel, TQueryDto, TDto>
		where TModel : class, IIdentifiable 
		where TDto : class, IIdentifiable 
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;
        private readonly IQueryService<TModel, TQueryDto> queryService;


		public GenericGetListService(IDbContext context, IMapper mapper, IQueryService<TModel, TQueryDto> queryService)
        {
            this.context = context;
            this.mapper = mapper;
            this.queryService = queryService;
        }

        public async Task<IEnumerable<TDto>> GetAsync(IListQueryDto dto)
        {
            return await QueryContext((TQueryDto)dto)
                .OrderBy(m => m.Id)
                .Skip((dto.Page - 1) * dto.PageSize).Take(dto.PageSize)
                .ProjectTo<TDto>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
		
        private IQueryable<TModel> QueryContext(TQueryDto dto)
        {
            return context.Set<TModel>()
                .AsNoTracking()
                .Where(queryService.WhereQuery(context, dto));
        }
    }
}

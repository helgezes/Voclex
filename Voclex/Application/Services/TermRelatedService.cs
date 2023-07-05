using Application.DataAccess;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.ModelInterfaces;

namespace Application.Services
{
	public class TermRelatedService<TModel, TDto> : ITermRelatedService<TDto> where TModel : class, ITermRelated
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;

        public TermRelatedService(IDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetListAsync(int[] termsIds)
        {
            return await QueryContextAndProjectToDto(termsIds)
                .ToArrayAsync();
        }
        
        private IQueryable<TDto> QueryContextAndProjectToDto(int[] termsIds)
        {
            return QueryContext(termsIds)
                .ProjectTo<TDto>(mapper.ConfigurationProvider);
        }

        private IQueryable<TModel> QueryContext(int[] termsIds)
        {
            return context.Set<TModel>()
                .AsNoTracking()
                .Where(t => termsIds.Contains(t.TermId));
        }
    }
}

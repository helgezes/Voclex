using Application.DataAccess;
using Application.ModelInterfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class GenericCrudService<TModel, TDto> where TModel : class, IIdentifiable where TDto : class, IIdentifiable
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;

        public GenericCrudService(IDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TModel> Create(TDto dto, bool saveChanges = true)
        {
            var mappedModel = mapper.Map<TModel>(dto);

            await context.Set<TModel>().AddAsync(mappedModel);

            if (saveChanges) await context.SaveChangesAsync();

            return mappedModel;
        }
        
        public async Task<TDto?> GetByIdAsync(int id)
        {
            var model = await context
                .Set<TModel>().AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return model != null ? mapper.Map<TDto>(model) : null;
        }

        public async Task<TModel?> UpdateAsync(TDto dto, bool saveChanges = true)
        {
            var modelFromDb =  await context.Set<TModel>().FindAsync(dto.Id);
            if (modelFromDb == null) return null;

            mapper.Map(dto, modelFromDb);
            if (saveChanges) await context.SaveChangesAsync();

            return modelFromDb;
        }

        public async Task<bool> Delete(int id, bool saveChanges = true)
        {
            var entity = await context.Set<TModel>().FindAsync(id);
            var entityExists = entity != null;
            if (entityExists)
                context.Set<TModel>().Remove(entity!);

            if (entityExists && saveChanges) await context.SaveChangesAsync();

            return entityExists;
        }
    }
}

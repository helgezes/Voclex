using SharedLibrary.ModelInterfaces;

namespace Application.Services.Interfaces;

public interface ICrudService<TModel, TDto> where TModel : class, IIdentifiable where TDto : class, IIdentifiable
{
	Task<TModel> Create(TDto dto, bool saveChanges = true);
	Task<TDto?> GetByIdAsync(int id);
	Task<TModel?> UpdateAsync(TDto dto, bool saveChanges = true);
	Task<bool> Delete(int id, bool saveChanges = true);
}
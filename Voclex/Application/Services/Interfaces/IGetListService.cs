using SharedLibrary.ModelInterfaces;

namespace Application.Services.Interfaces;

public interface IGetListService<TModel, TDto> where TModel : class, IIdentifiable where TDto : class, IIdentifiable
{
	Task<IEnumerable<TDto>> GetAsync(IListQuery<TModel> query);
	Task<int> GetCount(IQuery<TModel> query);
}
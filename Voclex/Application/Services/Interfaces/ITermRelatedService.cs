namespace Application.Services.Interfaces;

public interface ITermRelatedService<TDto>
{
	Task<IEnumerable<TDto>> GetListAsync(int[] termsIds);
}
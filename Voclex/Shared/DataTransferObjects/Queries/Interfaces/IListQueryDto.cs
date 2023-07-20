namespace SharedLibrary.DataTransferObjects.Queries.Interfaces;

public interface IListQueryDto
{
	int Page { get; }

	int PageSize { get; }
}
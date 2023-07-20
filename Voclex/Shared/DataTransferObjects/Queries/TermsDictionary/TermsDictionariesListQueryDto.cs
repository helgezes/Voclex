using SharedLibrary.DataTransferObjects.Queries.Interfaces;

namespace SharedLibrary.DataTransferObjects.Queries.TermsDictionary
{
	public sealed class TermsDictionariesListQueryDto : TermsDictionariesQueryDto, IListQueryDto
	{
        public TermsDictionariesListQueryDto(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        public TermsDictionariesListQueryDto() {}


        public int Page { get; init; }

        public int PageSize { get; init; }
    }
}

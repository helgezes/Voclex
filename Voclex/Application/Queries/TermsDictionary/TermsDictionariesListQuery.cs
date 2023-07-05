using Application.Services;

namespace Application.Queries.TermsDictionary
{
	public sealed class TermsDictionariesListQuery : TermsDictionariesQuery, IListQuery<Application.Models.TermsDictionary>
	{
        public TermsDictionariesListQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        public TermsDictionariesListQuery() {}


        public int Page { get; init; }

        public int PageSize { get; init; }
    }
}

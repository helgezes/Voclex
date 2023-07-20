using SharedLibrary.DataTransferObjects.Queries.Interfaces;

namespace SharedLibrary.DataTransferObjects.Queries.Terms;

public class TermsListQueryDto : TermsQueryDto, IListQueryDto
{
    public TermsListQueryDto(int page, int pageSize,
        TermsListEnumQueryVariants queryVariant, int[]? dictionariesIds = null) : 
        base(queryVariant, dictionariesIds)
    {
        Page = page;
        PageSize = pageSize;
    }

    public TermsListQueryDto() { }

    public int Page { get; init; }

    public int PageSize { get; init; }
}
using Application.Models;
using Application.Services;

namespace SharedLibrary.Queries.Terms;

public class TermsListQuery : TermsQuery, IListQuery<Term>
{
    public TermsListQuery(int page, int pageSize,
        TermsListEnumQueryVariants queryVariant, int[] dictionariesIds) : 
        base(queryVariant, dictionariesIds)
    {
        Page = page;
        PageSize = pageSize;
    }

    public TermsListQuery() { }

    public int Page { get; init; }

    public int PageSize { get; init; }
}
using Application.Models;
using Application.Services;

namespace SharedLibrary.Queries.Terms;

public class TermsListQuery : TermsQuery, IListQuery<Term>
{
    public TermsListQuery(
        int page, int pageSize, int userId, 
        TermsListEnumQueryVariants queryVariant, int[] dictionariesIds) : 
        base(userId, queryVariant, dictionariesIds)
    {
        Page = page;
        PageSize = pageSize;
    }

    public TermsListQuery() { }

    public int Page { get; init; }

    public int PageSize { get; init; }
}
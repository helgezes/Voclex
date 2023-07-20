using SharedLibrary.Attributes;

namespace SharedLibrary.DataTransferObjects.Queries.Terms;

public class TermsQueryDto //todo refactor this ?
{
    public TermsQueryDto(TermsListEnumQueryVariants queryVariant, int[]? dictionariesIds = null)
    {
        QueryVariant = queryVariant;
        DictionariesIds = dictionariesIds;//TODO settings DictionariesIds
    }

    public TermsQueryDto(){}

    public int[]? DictionariesIds { get; init; }

    [UserId]
    public int UserId { get; init; }

    public TermsListEnumQueryVariants QueryVariant { get; init; }
}
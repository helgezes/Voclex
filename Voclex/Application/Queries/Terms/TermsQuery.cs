using System.Linq.Expressions;
using Application.DataAccess;
using Application.Models;
using Application.Services;
using SharedLibrary.Attributes;

namespace Application.Queries.Terms;

public class TermsQuery : IQuery<Term> //todo refactor this ?
{
    public TermsQuery(TermsListEnumQueryVariants queryVariant, int[]? dictionariesIds = null)
    {
        QueryVariant = queryVariant;
        DictionariesIds = dictionariesIds;//TODO settings DictionariesIds
    }

    public TermsQuery(){}

    public int[]? DictionariesIds { get; init; }

    [UserId]
    public int UserId { get; init; }

    public TermsListEnumQueryVariants QueryVariant { get; init; }


    public Expression<Func<Term, bool>> WhereQuery(IDbContext context) //we might separate query data and the method using it in the future, right now don't see a compelling reason for that 
    {
        switch (QueryVariant)
        {
            case TermsListEnumQueryVariants.GetOnlyNew:
                var knownTermIdsForThatUser = context.TermProgresses
                    .Where(t => t.UserId == UserId)
                    .Select(x => x.TermId).ToArray(); //todo check performance of implementation in one query with usage of navigational properties. should be checked on prod data.
                return term => term.TermsDictionaryId != 1 && (term.TermsDictionary.UserId == UserId || term.TermsDictionary.UserId == null) &&
                               !knownTermIdsForThatUser.Contains(term.Id);

            case TermsListEnumQueryVariants.GetOnlyForRepetition:
                var currentDateTime = DateTimeOffset.UtcNow;
                
                var termIdsInProgress = context.TermProgresses
                    .Join(context.GuessedTimesCountToHoursWaiting, 
                        p => p.GuessedTimesCount, 
                        g => g.GuessedTimesCount, 
                        (p, g) => new { TermProgress = p, GuessedTimesToHoursWaiting = g })
                    .Where(x => x.TermProgress.UserId == UserId &&
                                currentDateTime >= x.TermProgress.LastGuessDateTime.AddHours(x.GuessedTimesToHoursWaiting.HoursWaiting))
                    .Select(x => x.TermProgress.TermId).ToArray();
                
                return term => (term.TermsDictionary.UserId == UserId || term.TermsDictionary.UserId == null) && //userid = null are shared dictionaries
                               termIdsInProgress.Contains(term.Id);

            case TermsListEnumQueryVariants.GetAll:
                return term => (term.TermsDictionary.UserId == UserId || term.TermsDictionary.UserId == null) && DictionariesIds.Contains(term.TermsDictionaryId);

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
using System.Linq.Expressions;
using Application.DataAccess;
using Application.Models;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace SharedLibrary.Queries.Terms;

public class TermsQuery : IQuery<Term> //todo refactor this ?
{
    public TermsQuery(int userId, TermsListEnumQueryVariants queryVariant, int[] dictionariesIds)
    {
        UserId = userId;
        DictionariesIds = dictionariesIds;
        QueryVariant = queryVariant;
    }

    public TermsQuery(){}

    public int[] DictionariesIds { get; init; }

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
                return term => DictionariesIds.Contains(term.TermsDictionaryId) &&
                               !knownTermIdsForThatUser.Contains(term.Id);

            case TermsListEnumQueryVariants.GetOnlyForRepetition:
                var currentDateTime = DateTime.Now;
                
                var termIdsInProgress = context.TermProgresses
                    .Join(context.GuessedTimesCountToHoursWaiting, 
                        p => p.GuessedTimesCount, 
                        g => g.GuessedTimesCount, 
                        (p, g) => new { TermProgress = p, GuessedTimesToHoursWaiting = g })
                    .Where(x => x.TermProgress.UserId == UserId &&
                                currentDateTime >= x.TermProgress.LastGuessDateTime.AddHours(x.GuessedTimesToHoursWaiting.HoursWaiting))
                    .Select(x => x.TermProgress.TermId).ToArray();
                
                return term => DictionariesIds.Contains(term.TermsDictionaryId) &&
                               termIdsInProgress.Contains(term.Id);

            case TermsListEnumQueryVariants.GetAll:
                return term => DictionariesIds.Contains(term.TermsDictionaryId);

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
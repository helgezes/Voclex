using Application.DataAccess;
using Application.Models;
using System.Linq.Expressions;
using SharedLibrary.DataTransferObjects.Queries.Terms;

namespace Application.Services.QueryServices
{
	public sealed class TermsQueryService : IQueryService<Term, TermsQueryDto>
	{
		public Expression<Func<Term, bool>> WhereQuery(IDbContext context, TermsQueryDto dto)
		{
			switch (dto.QueryVariant)
			{
				case TermsListEnumQueryVariants.GetOnlyNew:
					var knownTermIdsForThatUser = context.TermProgresses
						.Where(t => t.UserId == dto.UserId)
						.Select(x => x.TermId).ToArray(); //todo check performance of implementation in one query with usage of navigational properties. should be checked on prod data.
					return term => (term.TermsDictionary.UserId == dto.UserId || term.TermsDictionary.UserId == null) &&
								   !knownTermIdsForThatUser.Contains(term.Id);

				case TermsListEnumQueryVariants.GetOnlyForRepetition:
					var currentDateTime = DateTimeOffset.UtcNow;

					var termIdsInProgress = context.TermProgresses
						.Join(context.GuessedTimesCountToHoursWaiting,
							p => p.GuessedTimesCount,
					g => g.GuessedTimesCount,
							(p, g) => new { TermProgress = p, GuessedTimesToHoursWaiting = g })
						.Where(x => x.TermProgress.UserId == dto.UserId &&
									currentDateTime >= x.TermProgress.LastGuessDateTime.AddHours(x.GuessedTimesToHoursWaiting.HoursWaiting))
						.Select(x => x.TermProgress.TermId).ToArray();

					return term => (term.TermsDictionary.UserId == dto.UserId || term.TermsDictionary.UserId == null) && //userid = null are shared dictionaries
								   termIdsInProgress.Contains(term.Id);

				case TermsListEnumQueryVariants.GetAll:
					return term => (term.TermsDictionary.UserId == dto.UserId || term.TermsDictionary.UserId == null) && dto.DictionariesIds.Contains(term.TermsDictionaryId);

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}

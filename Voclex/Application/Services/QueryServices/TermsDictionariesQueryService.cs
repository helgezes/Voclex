using System.Linq.Expressions;
using Application.DataAccess;
using Application.Models;
using SharedLibrary.DataTransferObjects.Queries.TermsDictionary;
using SharedLibrary.Enums;

namespace Application.Services.QueryServices
{
	public class TermsDictionariesQueryService : IQueryService<TermsDictionary, TermsDictionariesQueryDto>
	{
		public Expression<Func<TermsDictionary, bool>> WhereQuery(IDbContext context, TermsDictionariesQueryDto dto)
		{
			var userIsAdmin = context.Users.Any(u => u.Id == dto.UserId && u.Role == Role.Admin);
			if (userIsAdmin)
			{
				return (dictionary) => dictionary.UserId == null;
			}

			return (dictionary) => dictionary.UserId == dto.UserId;
		}
	}
}

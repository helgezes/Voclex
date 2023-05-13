using System.Linq.Expressions;
using Application.DataAccess;
using Application.Models;
using Application.Services;
using SharedLibrary.Attributes;

namespace SharedLibrary.Queries.TermsDictionary
{
	public class TermsDictionariesQuery : IQuery<Application.Models.TermsDictionary>
    {
        [UserId]
        public int UserId { get; init; }

        public Expression<Func<Application.Models.TermsDictionary, bool>> WhereQuery(IDbContext context)
        {
            var userIsAdmin = context.Users.Any(u => u.Id == UserId && u.Role == Role.Admin);
            if (userIsAdmin)
            {
                return (dictionary) => dictionary.UserId == null;
            }

            return (dictionary) => dictionary.UserId == UserId;
        }
    }
}

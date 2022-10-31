using System.Linq.Expressions;
using Application.DataAccess;
using Application.Services;

namespace Shared.Queries.TermsDictionary
{
	public class TermsDictionariesQuery : IQuery<Application.Models.TermsDictionary>
	{
        public Expression<Func<Application.Models.TermsDictionary, bool>> WhereQuery(IDbContext context)
        {
            return (_) => true;
        }
    }
}

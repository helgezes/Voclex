using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess
{
    public interface IDbContext //we do not need to abstract from EF, as it is already pretty good abstraction above database
    {
        DbSet<TermsDictionary> TermsDictionaries { get; }

        DbSet<Term> Terms { get; }

        DbSet<Definition> Definitions { get; }

        DbSet<User> Users { get; }

        DbSet<DictionaryItemProgress> DictionaryItemProgresses { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

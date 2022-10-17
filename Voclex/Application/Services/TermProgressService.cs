using Application.DataAccess;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public sealed class TermProgressService
    {
        private readonly IDbContext context;

        public TermProgressService(IDbContext context)
        {
            this.context = context;
        }

        public async Task CorrectGuess(int termId, int userId)
        {
            var relevantProgressItem = await GetTermProgress(termId, userId);
            relevantProgressItem.CorrectGuess();
            await context.SaveChangesAsync();
        }

        public async Task IncorrectGuess(int termId, int userId)
        {
            var relevantProgressItem = await GetTermProgress(termId, userId);
            relevantProgressItem.IncorrectGuess();
            await context.SaveChangesAsync();
        }

        public async Task AlreadyKnow(int termId, int userId)
        {
            var relevantProgressItem = await GetTermProgress(termId, userId);
            relevantProgressItem.AlreadyKnow();
            await context.SaveChangesAsync();
        }


        private async Task<TermProgress> GetTermProgress(int termId, int userId)
        {
            return await GetTermProgressFromDbIfExists(termId, userId) ??
                   await CreateNewTermProgress(termId, userId);
        }

        private async Task<TermProgress?> GetTermProgressFromDbIfExists(int termId, int userId)
        {
            var relevantProgressItem =
                await context.TermProgresses
                    .FirstOrDefaultAsync(i =>
                        i.TermId == termId && i.UserId == userId);
            return relevantProgressItem;
        }

        private async Task<TermProgress> CreateNewTermProgress(int termId, int userId)
        {
            var userFromDbTask = context.Users.FindAsync(userId);
            var termFromDbTask = context.Terms.FindAsync(termId);

            TermProgress relevantProgressItem =
                new (await userFromDbTask, await termFromDbTask);

            context.TermProgresses.Add(relevantProgressItem);
            return relevantProgressItem;
        }

    }
}

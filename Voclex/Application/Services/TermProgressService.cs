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

        private async Task<TermProgress> GetTermProgress(int dictionaryItemId, int userId)
        {
            return await GetTermProgressFromDbIfExists(dictionaryItemId, userId) ??
                   await CreateNewTermProgress(dictionaryItemId, userId);
        }

        private async Task<TermProgress?> GetTermProgressFromDbIfExists(int dictionaryItemId, int userId)
        {
            var relevantProgressItem =
                await context.TermProgresses
                    .FirstOrDefaultAsync(i =>
                        i.TermId == dictionaryItemId && i.UserId == userId);
            return relevantProgressItem;
        }

        private async Task<TermProgress> CreateNewTermProgress(int dictionaryItemId, int userId)
        {
            var userFromDbTask = context.Users.FindAsync(userId);
            var termFromDbTask = context.Terms.FindAsync(dictionaryItemId);

            TermProgress relevantProgressItem =
                new (await userFromDbTask, await termFromDbTask);

            context.TermProgresses.Add(relevantProgressItem);
            return relevantProgressItem;
        }

    }
}
